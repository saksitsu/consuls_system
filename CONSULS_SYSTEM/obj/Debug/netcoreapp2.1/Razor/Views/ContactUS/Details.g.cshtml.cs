#pragma checksum "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\ContactUS\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ca0d2cf0ba937dfe67dc4c933d5d4c7489bef140"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ContactUS_Details), @"mvc.1.0.view", @"/Views/ContactUS/Details.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/ContactUS/Details.cshtml", typeof(AspNetCore.Views_ContactUS_Details))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ca0d2cf0ba937dfe67dc4c933d5d4c7489bef140", @"/Views/ContactUS/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e9f6ffb3c63f73adacf82be85b9e0989819cffe0", @"/Views/_ViewImports.cshtml")]
    public class Views_ContactUS_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CONSULS_SYSTEM.Models.MT_ContactUS>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            BeginContext(43, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\ContactUS\Details.cshtml"
  
    ViewData["Title"] = "Details";

#line default
#line hidden
            BeginContext(88, 126, true);
            WriteLiteral("\r\n<h2>Details</h2>\r\n\r\n<div>\r\n    <h4>MT_ContactUS</h4>\r\n    <hr />\r\n    <dl class=\"dl-horizontal\">\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(215, 43, false);
#line 14 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\ContactUS\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Address));

#line default
#line hidden
            EndContext();
            BeginContext(258, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(302, 39, false);
#line 17 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\ContactUS\Details.cshtml"
       Write(Html.DisplayFor(model => model.Address));

#line default
#line hidden
            EndContext();
            BeginContext(341, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(385, 43, false);
#line 20 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\ContactUS\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Email_1));

#line default
#line hidden
            EndContext();
            BeginContext(428, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(472, 39, false);
#line 23 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\ContactUS\Details.cshtml"
       Write(Html.DisplayFor(model => model.Email_1));

#line default
#line hidden
            EndContext();
            BeginContext(511, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(555, 43, false);
#line 26 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\ContactUS\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Email_2));

#line default
#line hidden
            EndContext();
            BeginContext(598, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(642, 39, false);
#line 29 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\ContactUS\Details.cshtml"
       Write(Html.DisplayFor(model => model.Email_2));

#line default
#line hidden
            EndContext();
            BeginContext(681, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(725, 43, false);
#line 32 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\ContactUS\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Email_3));

#line default
#line hidden
            EndContext();
            BeginContext(768, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(812, 39, false);
#line 35 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\ContactUS\Details.cshtml"
       Write(Html.DisplayFor(model => model.Email_3));

#line default
#line hidden
            EndContext();
            BeginContext(851, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(895, 49, false);
#line 38 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\ContactUS\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Phone_Number1));

#line default
#line hidden
            EndContext();
            BeginContext(944, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(988, 45, false);
#line 41 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\ContactUS\Details.cshtml"
       Write(Html.DisplayFor(model => model.Phone_Number1));

#line default
#line hidden
            EndContext();
            BeginContext(1033, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1077, 49, false);
#line 44 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\ContactUS\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Phone_Number2));

#line default
#line hidden
            EndContext();
            BeginContext(1126, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1170, 45, false);
#line 47 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\ContactUS\Details.cshtml"
       Write(Html.DisplayFor(model => model.Phone_Number2));

#line default
#line hidden
            EndContext();
            BeginContext(1215, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1259, 49, false);
#line 50 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\ContactUS\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Phone_Number3));

#line default
#line hidden
            EndContext();
            BeginContext(1308, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1352, 45, false);
#line 53 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\ContactUS\Details.cshtml"
       Write(Html.DisplayFor(model => model.Phone_Number3));

#line default
#line hidden
            EndContext();
            BeginContext(1397, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1441, 50, false);
#line 56 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\ContactUS\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Google_Map_URL));

#line default
#line hidden
            EndContext();
            BeginContext(1491, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1535, 46, false);
#line 59 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\ContactUS\Details.cshtml"
       Write(Html.DisplayFor(model => model.Google_Map_URL));

#line default
#line hidden
            EndContext();
            BeginContext(1581, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1625, 42, false);
#line 62 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\ContactUS\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Active));

#line default
#line hidden
            EndContext();
            BeginContext(1667, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1711, 38, false);
#line 65 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\ContactUS\Details.cshtml"
       Write(Html.DisplayFor(model => model.Active));

#line default
#line hidden
            EndContext();
            BeginContext(1749, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1793, 44, false);
#line 68 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\ContactUS\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.Createby));

#line default
#line hidden
            EndContext();
            BeginContext(1837, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1881, 40, false);
#line 71 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\ContactUS\Details.cshtml"
       Write(Html.DisplayFor(model => model.Createby));

#line default
#line hidden
            EndContext();
            BeginContext(1921, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1965, 46, false);
#line 74 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\ContactUS\Details.cshtml"
       Write(Html.DisplayNameFor(model => model.CreateDate));

#line default
#line hidden
            EndContext();
            BeginContext(2011, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(2055, 42, false);
#line 77 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\ContactUS\Details.cshtml"
       Write(Html.DisplayFor(model => model.CreateDate));

#line default
#line hidden
            EndContext();
            BeginContext(2097, 47, true);
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n</div>\r\n<div>\r\n    ");
            EndContext();
            BeginContext(2144, 54, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "66e29babe6f74508a7f8d3b9865b42d4", async() => {
                BeginContext(2190, 4, true);
                WriteLiteral("Edit");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#line 82 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\ContactUS\Details.cshtml"
                           WriteLiteral(Model.ID);

#line default
#line hidden
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2198, 8, true);
            WriteLiteral(" |\r\n    ");
            EndContext();
            BeginContext(2206, 38, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "3f265e46199e4d8389ff10bc7cea3626", async() => {
                BeginContext(2228, 12, true);
                WriteLiteral("Back to List");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2244, 10, true);
            WriteLiteral("\r\n</div>\r\n");
            EndContext();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CONSULS_SYSTEM.Models.MT_ContactUS> Html { get; private set; }
    }
}
#pragma warning restore 1591
