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
using HQSOFT.eBiz.Inventor.LotSerSegments;
using HQSOFT.eBiz.Inventor.Permissions;
using HQSOFT.eBiz.Inventor.Shared;

namespace HQSOFT.eBiz.Inventor.Blazor.Pages.Inventor
{
    public partial class LotSerSegments
    {
        protected List<Volo.Abp.BlazoriseUI.BreadcrumbItem> BreadcrumbItems = new List<Volo.Abp.BlazoriseUI.BreadcrumbItem>();
        protected PageToolbar Toolbar {get;} = new PageToolbar();
        private IReadOnlyList<LotSerSegmentDto> LotSerSegmentList { get; set; }
        private int PageSize { get; } = LimitedResultRequestDto.DefaultMaxResultCount;
        private int CurrentPage { get; set; } = 1;
        private string CurrentSorting { get; set; } = string.Empty;
        private int TotalCount { get; set; }
        private bool CanCreateLotSerSegment { get; set; }
        private bool CanEditLotSerSegment { get; set; }
        private bool CanDeleteLotSerSegment { get; set; }
        private LotSerSegmentCreateDto NewLotSerSegment { get; set; }
        private Validations NewLotSerSegmentValidations { get; set; } = new();
        private LotSerSegmentUpdateDto EditingLotSerSegment { get; set; }
        private Validations EditingLotSerSegmentValidations { get; set; } = new();
        private Guid EditingLotSerSegmentId { get; set; }
        private Modal CreateLotSerSegmentModal { get; set; } = new();
        private Modal EditLotSerSegmentModal { get; set; } = new();
        private GetLotSerSegmentsInput Filter { get; set; }
        private DataGridEntityActionsColumn<LotSerSegmentDto> EntityActionsColumn { get; set; } = new();
        protected string SelectedCreateTab = "lotSerSegment-create-tab";
        protected string SelectedEditTab = "lotSerSegment-edit-tab";
        
        public LotSerSegments()
        {
            NewLotSerSegment = new LotSerSegmentCreateDto();
            EditingLotSerSegment = new LotSerSegmentUpdateDto();
            Filter = new GetLotSerSegmentsInput
            {
                MaxResultCount = PageSize,
                SkipCount = (CurrentPage - 1) * PageSize,
                Sorting = CurrentSorting
            };
            LotSerSegmentList = new List<LotSerSegmentDto>();
        }

        protected override async Task OnInitializedAsync()
        {
            await SetToolbarItemsAsync();
            await SetBreadcrumbItemsAsync();
            await SetPermissionsAsync();
        }

        protected virtual ValueTask SetBreadcrumbItemsAsync()
        {
            BreadcrumbItems.Add(new Volo.Abp.BlazoriseUI.BreadcrumbItem(L["Menu:LotSerSegments"]));
            return ValueTask.CompletedTask;
        }

        protected virtual ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["ExportToExcel"], async () =>{ await DownloadAsExcelAsync(); }, IconName.Download);
            
            Toolbar.AddButton(L["NewLotSerSegment"], async () =>
            {
                await OpenCreateLotSerSegmentModalAsync();
            }, IconName.Add, requiredPolicyName: InventorPermissions.LotSerSegments.Create);

            return ValueTask.CompletedTask;
        }

        private async Task SetPermissionsAsync()
        {
            CanCreateLotSerSegment = await AuthorizationService
                .IsGrantedAsync(InventorPermissions.LotSerSegments.Create);
            CanEditLotSerSegment = await AuthorizationService
                            .IsGrantedAsync(InventorPermissions.LotSerSegments.Edit);
            CanDeleteLotSerSegment = await AuthorizationService
                            .IsGrantedAsync(InventorPermissions.LotSerSegments.Delete);
        }

        private async Task GetLotSerSegmentsAsync()
        {
            Filter.MaxResultCount = PageSize;
            Filter.SkipCount = (CurrentPage - 1) * PageSize;
            Filter.Sorting = CurrentSorting;

            var result = await LotSerSegmentsAppService.GetListAsync(Filter);
            LotSerSegmentList = result.Items;
            TotalCount = (int)result.TotalCount;
        }

        protected virtual async Task SearchAsync()
        {
            CurrentPage = 1;
            await GetLotSerSegmentsAsync();
            await InvokeAsync(StateHasChanged);
        }

        private  async Task DownloadAsExcelAsync()
        {
            var token = (await LotSerSegmentsAppService.GetDownloadTokenAsync()).Token;
            var remoteService = await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("Inventor") ??
            await RemoteServiceConfigurationProvider.GetConfigurationOrDefaultOrNullAsync("Default");
            NavigationManager.NavigateTo($"{remoteService?.BaseUrl.EnsureEndsWith('/') ?? string.Empty}api/inventor/lot-ser-segments/as-excel-file?DownloadToken={token}&FilterText={Filter.FilterText}", forceLoad: true);
        }

        private async Task OnDataGridReadAsync(DataGridReadDataEventArgs<LotSerSegmentDto> e)
        {
            CurrentSorting = e.Columns
                .Where(c => c.SortDirection != SortDirection.Default)
                .Select(c => c.Field + (c.SortDirection == SortDirection.Descending ? " DESC" : ""))
                .JoinAsString(",");
            CurrentPage = e.Page;
            await GetLotSerSegmentsAsync();
            await InvokeAsync(StateHasChanged);
        }

        private async Task OpenCreateLotSerSegmentModalAsync()
        {
            NewLotSerSegment = new LotSerSegmentCreateDto{
                
                
            };
            await NewLotSerSegmentValidations.ClearAll();
            await CreateLotSerSegmentModal.Show();
        }

        private async Task CloseCreateLotSerSegmentModalAsync()
        {
            NewLotSerSegment = new LotSerSegmentCreateDto{
                
                
            };
            await CreateLotSerSegmentModal.Hide();
        }

        private async Task OpenEditLotSerSegmentModalAsync(LotSerSegmentDto input)
        {
            var lotSerSegment = await LotSerSegmentsAppService.GetAsync(input.Id);
            
            EditingLotSerSegmentId = lotSerSegment.Id;
            EditingLotSerSegment = ObjectMapper.Map<LotSerSegmentDto, LotSerSegmentUpdateDto>(lotSerSegment);
            await EditingLotSerSegmentValidations.ClearAll();
            await EditLotSerSegmentModal.Show();
        }

        private async Task DeleteLotSerSegmentAsync(LotSerSegmentDto input)
        {
            await LotSerSegmentsAppService.DeleteAsync(input.Id);
            await GetLotSerSegmentsAsync();
        }

        private async Task CreateLotSerSegmentAsync()
        {
            try
            {
                if (await NewLotSerSegmentValidations.ValidateAll() == false)
                {
                    return;
                }

                await LotSerSegmentsAppService.CreateAsync(NewLotSerSegment);
                await GetLotSerSegmentsAsync();
                await CloseCreateLotSerSegmentModalAsync();
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }

        private async Task CloseEditLotSerSegmentModalAsync()
        {
            await EditLotSerSegmentModal.Hide();
        }

        private async Task UpdateLotSerSegmentAsync()
        {
            try
            {
                if (await EditingLotSerSegmentValidations.ValidateAll() == false)
                {
                    return;
                }

                await LotSerSegmentsAppService.UpdateAsync(EditingLotSerSegmentId, EditingLotSerSegment);
                await GetLotSerSegmentsAsync();
                await EditLotSerSegmentModal.Hide();                
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
        

    }
}
