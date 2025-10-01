using PreimerClinc.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace PreimerClinc.Permissions;

public class PreimerClincPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(PreimerClincPermissions.GroupName);


        var booksPermission = myGroup.AddPermission(PreimerClincPermissions.Books.Default, L("Permission:Books"));
        booksPermission.AddChild(PreimerClincPermissions.Books.Create, L("Permission:Books.Create"));
        booksPermission.AddChild(PreimerClincPermissions.Books.Edit, L("Permission:Books.Edit"));
        booksPermission.AddChild(PreimerClincPermissions.Books.Delete, L("Permission:Books.Delete"));

        //Define your own permissions here. Example:
        //myGroup.AddPermission(PreimerClincPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<PreimerClincResource>(name);
    }
}
