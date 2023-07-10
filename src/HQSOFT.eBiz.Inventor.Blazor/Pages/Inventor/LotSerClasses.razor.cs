using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Blazorise.DataGrid;
using Volo.Abp.BlazoriseUI.Components;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using HQSOFT.eBiz.Inventor.LotSerClasses;
using HQSOFT.eBiz.Inventor.Permissions;
using HQSOFT.eBiz.Inventor.Shared;
using HQSOFT.eBiz.Inventor.Lots;

namespace HQSOFT.eBiz.Inventor.Blazor.Pages.Inventor
{
    public partial class LotSerClasses
    {
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar {get;} = new PageToolbar();
        private IReadOnlyList<LotSerClassDto> LotSerClassList { get; set; }
        private List<LotSerClassDto> selectedLotSerClass;
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; } = string.Empty;
        private int TotalCount { get; set; }
        private bool CanCreateLotSerClass { get; set; }
        private bool CanEditLotSerClass { get; set; }
        private bool CanDeleteLotSerClass { get; set; }
        private LotSerClassCreateDto NewLotSerClass { get; set; }
        private Validations NewLotSerClassValidations { get; set; } = new();
        private LotSerClassUpdateDto EditingLotSerClass { get; set; }
        private Validations EditingLotSerClassValidations { get; set; } = new();
        private Guid EditingLotSerClassId { get; set; }
        private Modal CreateLotSerClassModal { get; set; } = new();
        private Modal EditLotSerClassModal { get; set; } = new();
        private GetLotSerClassesInput Filter { get; set; }
        private DataGridEntityActionsColumn<LotSerClassDto> EntityActionsColumn { get; set; } = new();
        protected string SelectedCreateTab = "lotSerClass-create-tab";
        protected string SelectedEditTab = "lotSerClass-edit-tab";
        
        public LotSerClasses()
        {
            NewLotSerClass = new LotSerClassCreateDto();
            EditingLotSerClass = new LotSerClassUpdateDto();
            Filter = new GetLotSerClassesInput
            {
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
            LotSerClassList = new List<LotSerClassDto>();
        }

        protected override async Task OnInitializedAsync()
        {
            await SetToolbarItemsAsync();
            await SetBreadcrumbItemsAsync();
            await SetPermissionsAsync();
        }

        protected virtual ValueTask SetBreadcrumbItemsAsync()
        {
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:LotSerClasses"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["ExportToExcel"], async () =>{ await DownloadAsExcelAsync(); }, IconName.Download);
            
            Toolbar.AddButton(L["NewLotSerClass"], async () =>
            {
                await OpenCreateLotSerClassModalAsync();
            }, IconName.Add, requiredPolicyName: InventorPermissions.LotSerClasses.Create);

            return ValueTask.CompletedTask;
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateLotSerClass = await AuthorizationService
                .IsGrantedAsync(InventorPermissions.LotSerClasses.Create);
            CanEditLotSerClass = await AuthorizationService
                            .IsGrantedAsync(InventorPermissions.LotSerClasses.Edit);
            CanDeleteLotSerClass = await AuthorizationService
                            .IsGrantedAsync(InventorPermissions.LotSerClasses.Delete);
        }

        private async Task GetLotSerClassesAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;

            var result = await LotSerClassesAppService.GetListAsync(Filter);
            LotSerClassList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetLotSerClassesAsync();
            await InvokeAsync(StateHasChanged);
        }

        private  async Task DownloadAsExcelAsync()
        {
            var token = (await LotSerClassesAppService.GetDownloadTokenAsync()).Token;
            var remoteService = await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("Inventor") ??
            await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("Default");
            NavigationManager.NavigateTo($"{remoteService?.BaseUrl.EnsureEndsWith('/') ?? string.Empty}api/inventor/lot-ser-classes/as-excel-file?DownloadToken={token}&FilterText={Filter.FilterText}", forceLoad: true);
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<LotSerClassDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;
            await GetLotSerClassesAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OpenCreateLotSerClassModalAsync()
        {
            NewLotSerClass = new LotSerClassCreateDto{
                
                
            };
            await NewLotSerClassValidations.ClearAll();
            await CreateLotSerClassModal.Show();
        }

        private async Task CloseCreateLotSerClassModalAsync()
        {
            NewLotSerClass = new LotSerClassCreateDto{
                
                
            };
            await CreateLotSerClassModal.Hide();
        }

        private async Task OpenEditLotSerClassModalAsync(LotSerClassDto input)
        {
            var lotSerClass = await LotSerClassesAppService.GetAsync(input.Id);
            
            EditingLotSerClassId = lotSerClass.Id;
            EditingLotSerClass = ObjectMapper.Map<LotSerClassDto, LotSerClassUpdateDto>(lotSerClass);
            await EditingLotSerClassValidations.ClearAll();
            await EditLotSerClassModal.Show();
        }

        private async Task DeleteLotSerClassAsync(LotSerClassDto input)
        {
            await LotSerClassesAppService.DeleteAsync(input.Id);
            await GetLotSerClassesAsync();
        }

        private async Task CreateLotSerClassAsync()
        {
            try
            {
                if (await NewLotSerClassValidations.ValidateAll() == false)
                {
                    return;
                }

                await LotSerClassesAppService.CreateAsync(NewLotSerClass);
                await GetLotSerClassesAsync();
                await CloseCreateLotSerClassModalAsync();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private async Task CloseEditLotSerClassModalAsync()
        {
            await EditLotSerClassModal.Hide();
        }

        private async Task UpdateLotSerClassAsync()
        {
            try
            {
                if (await EditingLotSerClassValidations.ValidateAll() == false)
                {
                    return;
                }

                await LotSerClassesAppService.UpdateAsync(EditingLotSerClassId, EditingLotSerClass);
                await GetLotSerClassesAsync();
                await EditLotSerClassModal.Hide();                
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private void OnSelectedCreateTabChanged(string name)
        {
            SelectedCreateTab = name;
        }

        private void OnSelectedEditTabChanged(string name)
        {
            SelectedEditTab = name;
        }
        //private bool IsAutoIncremetalValueVisible()
        //{
        //    return EditingLotSerClass.TrackingMethod == TrackingMethod.S
        //        && EditingLotSerClass.AssignMethod == AssignMethod.U
        //        && EditingLotSerClass.ShareAutoIncremenitalValueBetwenAllClassItems;
        //}

        //private bool IsMaxAutoNumbersVisible()
        //{
        //    return EditingLotSerClass.TrackingMethod == TrackingMethod.S
        //        && EditingLotSerClass.AssignMethod == AssignMethod.U
        //        && EditingLotSerClass.ShareAutoIncremenitalValueBetwenAllClassItems;
        //}
        protected void GoToEditPage(LotSerClassDto context)
        {
            NavigationManager.NavigateTo($"/Inventor/LotSerClasses/{context.Id}");
        }

    }
}
