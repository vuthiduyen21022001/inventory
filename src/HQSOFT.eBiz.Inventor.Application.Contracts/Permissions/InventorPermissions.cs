using Volo.Abp.Reflection;

namespace HQSOFT.eBiz.Inventor.Permissions;

public class InventorPermissions
{
    public const string GroupName = "Inventor";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(InventorPermissions));
    }

    public static class LotSerClasses
    {
        public const string Default = GroupName + ".LotSerClasses";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public static class LotSerSegments
    {
        public const string Default = GroupName + ".LotSerSegments";
        public const string Edit = Default + ".Edit";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
}