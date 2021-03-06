#pragma checksum "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Shared\_BindingMenu_admin.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9a3c7c2b5c5fd59b63fe20db3874a0c27e381f51"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__BindingMenu_admin), @"mvc.1.0.view", @"/Views/Shared/_BindingMenu_admin.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/_BindingMenu_admin.cshtml", typeof(AspNetCore.Views_Shared__BindingMenu_admin))]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#line 1 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\_ViewImports.cshtml"
using CONSULS_SYSTEM;

#line default
#line hidden
#line 2 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\_ViewImports.cshtml"
using CONSULS_SYSTEM.Models;

#line default
#line hidden
#line 1 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Shared\_BindingMenu_admin.cshtml"
using CONSULS_SYSTEM.Resources;

#line default
#line hidden
#line 2 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Shared\_BindingMenu_admin.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#line 3 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Shared\_BindingMenu_admin.cshtml"
using Newtonsoft.Json;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"9a3c7c2b5c5fd59b63fe20db3874a0c27e381f51", @"/Views/Shared/_BindingMenu_admin.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e9f6ffb3c63f73adacf82be85b9e0989819cffe0", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__BindingMenu_admin : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(93, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 6 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Shared\_BindingMenu_admin.cshtml"
  
    var Json_menu = Context.Session.GetString("session_menu");

    var List_Menu = new List<CONSULS_SYSTEM.Models.TB_Menu_Authorize>();
    if (Json_menu != null)
    {
        List_Menu = JsonConvert.DeserializeObject<List<CONSULS_SYSTEM.Models.TB_Menu_Authorize>>(Json_menu);
    }

    var Group_Menu = List_Menu.Where(x => x.menu_info.ChildOfMenu == "" || x.menu_info.ChildOfMenu == null).ToList();

#line default
#line hidden
            BeginContext(551, 118, true);
            WriteLiteral("\r\n<ul class=\"sidebar-menu\" data-widget=\"tree\" id=\"menu\" style=\"display:none\">\r\n    <li class=\"header\">MAIN MENU</li>\r\n");
            EndContext();
#line 20 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Shared\_BindingMenu_admin.cshtml"
     foreach (var item in Group_Menu)
    {
        string group = item.menu_info.Menu_ID;
        var sub_menu = List_Menu.Where(x => x.menu_info.ChildOfMenu == group).ToList();

        

#line default
#line hidden
#line 25 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Shared\_BindingMenu_admin.cshtml"
         if (sub_menu.Count > 0)
        {

#line default
#line hidden
            BeginContext(899, 51, true);
            WriteLiteral("            <li class=\"treeview\">\r\n                ");
            EndContext();
            BeginContext(950, 392, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "da799f0a31654525b89b37ba1ffe0a2b", async() => {
                BeginContext(1033, 24, true);
                WriteLiteral("\r\n                    <i");
                EndContext();
                BeginWriteAttribute("class", " class=\"", 1057, "\"", 1085, 1);
#line 29 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Shared\_BindingMenu_admin.cshtml"
WriteAttributeValue("", 1065, item.menu_info.Icon, 1065, 20, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(1086, 12, true);
                WriteLiteral("></i> <span>");
                EndContext();
                BeginContext(1099, 59, false);
#line 29 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Shared\_BindingMenu_admin.cshtml"
                                                          Write(SharedLocalizer.GetLocalizedHtmlString(item.menu_info.Name));

#line default
#line hidden
                EndContext();
                BeginContext(1158, 180, true);
                WriteLiteral("</span>\r\n                    <span class=\"pull-right-container\">\r\n                        <i class=\"fa fa-angle-left pull-right\"></i>\r\n                    </span>\r\n                ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            BeginWriteTagHelperAttribute();
#line 28 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Shared\_BindingMenu_admin.cshtml"
                       WriteLiteral(item.menu_info.Controller);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-controller", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#line 28 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Shared\_BindingMenu_admin.cshtml"
                                                               WriteLiteral(item.menu_info.Action);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-action", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1342, 48, true);
            WriteLiteral("\r\n                <ul class=\"treeview-menu\">\r\n\r\n");
            EndContext();
#line 36 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Shared\_BindingMenu_admin.cshtml"
                     foreach (var sub_item in sub_menu)
                    {
                        string sub_group = sub_item.menu_info.Menu_ID;
                        var sub_sub_menu = List_Menu.Where(x => x.menu_info.ChildOfMenu == sub_group).ToList();
                        

#line default
#line hidden
#line 40 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Shared\_BindingMenu_admin.cshtml"
                         if (sub_sub_menu.Count > 0)
                        {

#line default
#line hidden
            BeginContext(1736, 83, true);
            WriteLiteral("                            <li class=\"treeview\">\r\n                                ");
            EndContext();
            BeginContext(1819, 461, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "65846af10fe04417aa807b1ae6929cf4", async() => {
                BeginContext(1906, 69, true);
                WriteLiteral("\r\n                                    <i class=\"fa fa-circle-o\"></i> ");
                EndContext();
                BeginContext(1976, 63, false);
#line 44 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Shared\_BindingMenu_admin.cshtml"
                                                              Write(SharedLocalizer.GetLocalizedHtmlString(sub_item.menu_info.Name));

#line default
#line hidden
                EndContext();
                BeginContext(2039, 237, true);
                WriteLiteral("\r\n                                    <span class=\"pull-right-container\">\r\n                                        <i class=\"fa fa-angle-left pull-right\"></i>\r\n                                    </span>\r\n                                ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            BeginWriteTagHelperAttribute();
#line 43 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Shared\_BindingMenu_admin.cshtml"
                                       WriteLiteral(sub_item.menu_info.Controller);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-controller", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#line 43 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Shared\_BindingMenu_admin.cshtml"
                                                                                   WriteLiteral(item.menu_info.Action);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-action", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2280, 62, true);
            WriteLiteral("\r\n                                <ul class=\"treeview-menu\">\r\n");
            EndContext();
#line 50 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Shared\_BindingMenu_admin.cshtml"
                                     foreach (var sub_sub_item in sub_sub_menu)
                                    {

#line default
#line hidden
            BeginContext(2462, 90, true);
            WriteLiteral("                                        <li>\r\n                                            ");
            EndContext();
            BeginContext(2552, 384, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "be741bcb022944868cd725bee8a4cc68", async() => {
                BeginContext(2651, 160, true);
                WriteLiteral("\r\n                                                <i class=\"fa fa-circle-o\"></i>\r\n                                                <span style=\"font-size:11px;\">");
                EndContext();
                BeginContext(2812, 67, false);
#line 55 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Shared\_BindingMenu_admin.cshtml"
                                                                         Write(SharedLocalizer.GetLocalizedHtmlString(sub_sub_item.menu_info.Name));

#line default
#line hidden
                EndContext();
                BeginContext(2879, 53, true);
                WriteLiteral("</span>\r\n                                            ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            BeginWriteTagHelperAttribute();
#line 53 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Shared\_BindingMenu_admin.cshtml"
                                                   WriteLiteral(sub_sub_item.menu_info.Controller);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-controller", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#line 53 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Shared\_BindingMenu_admin.cshtml"
                                                                                                   WriteLiteral(sub_sub_item.menu_info.Action);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-action", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2936, 49, true);
            WriteLiteral("\r\n                                        </li>\r\n");
            EndContext();
#line 58 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Shared\_BindingMenu_admin.cshtml"
                                    }

#line default
#line hidden
            BeginContext(3024, 74, true);
            WriteLiteral("                                </ul>\r\n                            </li>\r\n");
            EndContext();
#line 61 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Shared\_BindingMenu_admin.cshtml"
                        }
                        else
                        {

#line default
#line hidden
            BeginContext(3182, 32, true);
            WriteLiteral("                            <li>");
            EndContext();
            BeginContext(3214, 236, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "cb012682af784f37a51ca08f8706c0e4", async() => {
                BeginContext(3305, 31, true);
                WriteLiteral("<i class=\"fa fa-circle-o\"></i> ");
                EndContext();
                BeginContext(3337, 63, false);
#line 64 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Shared\_BindingMenu_admin.cshtml"
                                                                                                                                                     Write(SharedLocalizer.GetLocalizedHtmlString(sub_item.menu_info.Name));

#line default
#line hidden
                EndContext();
                BeginContext(3400, 5, true);
                WriteLiteral("<span");
                EndContext();
                BeginWriteAttribute("class", " class=\"", 3405, "\"", 3437, 1);
#line 64 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Shared\_BindingMenu_admin.cshtml"
WriteAttributeValue("", 3413, sub_item.menu_info.Icon, 3413, 24, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(3438, 8, true);
                WriteLiteral("></span>");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            BeginWriteTagHelperAttribute();
#line 64 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Shared\_BindingMenu_admin.cshtml"
                                       WriteLiteral(sub_item.menu_info.Controller);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-controller", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#line 64 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Shared\_BindingMenu_admin.cshtml"
                                                                                   WriteLiteral(sub_item.menu_info.Action);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-action", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3450, 7, true);
            WriteLiteral("</li>\r\n");
            EndContext();
#line 65 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Shared\_BindingMenu_admin.cshtml"
                        }

#line default
#line hidden
#line 65 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Shared\_BindingMenu_admin.cshtml"
                         
                    }

#line default
#line hidden
            BeginContext(3507, 42, true);
            WriteLiteral("                </ul>\r\n            </li>\r\n");
            EndContext();
#line 69 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Shared\_BindingMenu_admin.cshtml"
        }
        else
        {

#line default
#line hidden
            BeginContext(3585, 34, true);
            WriteLiteral("            <li>\r\n                ");
            EndContext();
            BeginContext(3619, 215, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "5da875444c664a2e8498c24786577fda", async() => {
                BeginContext(3702, 2, true);
                WriteLiteral("<i");
                EndContext();
                BeginWriteAttribute("class", " class=\"", 3704, "\"", 3732, 1);
#line 73 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Shared\_BindingMenu_admin.cshtml"
WriteAttributeValue("", 3712, item.menu_info.Icon, 3712, 20, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(3733, 30, true);
                WriteLiteral("></i> <span class=\"nav-label\">");
                EndContext();
                BeginContext(3764, 59, false);
#line 73 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Shared\_BindingMenu_admin.cshtml"
                                                                                                                                                           Write(SharedLocalizer.GetLocalizedHtmlString(item.menu_info.Name));

#line default
#line hidden
                EndContext();
                BeginContext(3823, 7, true);
                WriteLiteral("</span>");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            BeginWriteTagHelperAttribute();
#line 73 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Shared\_BindingMenu_admin.cshtml"
                       WriteLiteral(item.menu_info.Controller);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-controller", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            BeginWriteTagHelperAttribute();
#line 73 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Shared\_BindingMenu_admin.cshtml"
                                                               WriteLiteral(item.menu_info.Action);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-action", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3834, 21, true);
            WriteLiteral("\r\n            </li>\r\n");
            EndContext();
#line 75 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Shared\_BindingMenu_admin.cshtml"
        }

#line default
#line hidden
#line 75 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Shared\_BindingMenu_admin.cshtml"
         
    }

#line default
#line hidden
            BeginContext(3873, 318, true);
            WriteLiteral(@"
</ul>
<script>
    (function () {
            ContinueToRedirect();
        }());

        function ContinueToRedirect() {
            setTimeout(function () {
                var menu = document.getElementById(""menu"");
                menu.style.display = """";
            }, 1000);
        }
</script>
");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public LocService SharedLocalizer { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
