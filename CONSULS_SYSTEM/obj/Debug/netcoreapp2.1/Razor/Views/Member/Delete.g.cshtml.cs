#pragma checksum "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Member\Delete.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "02efe69de6663083d6cf5a417d6332749eafaa8b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Member_Delete), @"mvc.1.0.view", @"/Views/Member/Delete.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Member/Delete.cshtml", typeof(AspNetCore.Views_Member_Delete))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"02efe69de6663083d6cf5a417d6332749eafaa8b", @"/Views/Member/Delete.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e9f6ffb3c63f73adacf82be85b9e0989819cffe0", @"/Views/_ViewImports.cshtml")]
    public class Views_Member_Delete : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CONSULS_SYSTEM.Models.TB_Member>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "hidden", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(40, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 3 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Member\Delete.cshtml"
  
    Layout = "_Layout_admin";
    ViewData["Title"] = "Delete";

#line default
#line hidden
            BeginContext(115, 170, true);
            WriteLiteral("\r\n<h2>Delete</h2>\r\n\r\n<h3>Are you sure you want to delete this?</h3>\r\n<div>\r\n    <h4>TB_Member</h4>\r\n    <hr />\r\n    <dl class=\"dl-horizontal\">\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(286, 43, false);
#line 16 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Member\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.User_ID));

#line default
#line hidden
            EndContext();
            BeginContext(329, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(373, 39, false);
#line 19 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Member\Delete.cshtml"
       Write(Html.DisplayFor(model => model.User_ID));

#line default
#line hidden
            EndContext();
            BeginContext(412, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(456, 40, false);
#line 22 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Member\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Name));

#line default
#line hidden
            EndContext();
            BeginContext(496, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(540, 36, false);
#line 25 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Member\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Name));

#line default
#line hidden
            EndContext();
            BeginContext(576, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(620, 44, false);
#line 28 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Member\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Sub_Name));

#line default
#line hidden
            EndContext();
            BeginContext(664, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(708, 40, false);
#line 31 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Member\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Sub_Name));

#line default
#line hidden
            EndContext();
            BeginContext(748, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(792, 46, false);
#line 34 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Member\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Country_ID));

#line default
#line hidden
            EndContext();
            BeginContext(838, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(882, 42, false);
#line 37 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Member\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Country_ID));

#line default
#line hidden
            EndContext();
            BeginContext(924, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(968, 44, false);
#line 40 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Member\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Position));

#line default
#line hidden
            EndContext();
            BeginContext(1012, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1056, 40, false);
#line 43 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Member\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Position));

#line default
#line hidden
            EndContext();
            BeginContext(1096, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1140, 43, false);
#line 46 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Member\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Address));

#line default
#line hidden
            EndContext();
            BeginContext(1183, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1227, 39, false);
#line 49 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Member\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Address));

#line default
#line hidden
            EndContext();
            BeginContext(1266, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1310, 45, false);
#line 52 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Member\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Education));

#line default
#line hidden
            EndContext();
            BeginContext(1355, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1399, 41, false);
#line 55 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Member\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Education));

#line default
#line hidden
            EndContext();
            BeginContext(1440, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1484, 44, false);
#line 58 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Member\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Level_ID));

#line default
#line hidden
            EndContext();
            BeginContext(1528, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1572, 40, false);
#line 61 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Member\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Level_ID));

#line default
#line hidden
            EndContext();
            BeginContext(1612, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1656, 42, false);
#line 64 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Member\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Active));

#line default
#line hidden
            EndContext();
            BeginContext(1698, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1742, 38, false);
#line 67 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Member\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Active));

#line default
#line hidden
            EndContext();
            BeginContext(1780, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1824, 44, false);
#line 70 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Member\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.Createby));

#line default
#line hidden
            EndContext();
            BeginContext(1868, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(1912, 40, false);
#line 73 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Member\Delete.cshtml"
       Write(Html.DisplayFor(model => model.Createby));

#line default
#line hidden
            EndContext();
            BeginContext(1952, 43, true);
            WriteLiteral("\r\n        </dd>\r\n        <dt>\r\n            ");
            EndContext();
            BeginContext(1996, 46, false);
#line 76 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Member\Delete.cshtml"
       Write(Html.DisplayNameFor(model => model.CreateDate));

#line default
#line hidden
            EndContext();
            BeginContext(2042, 43, true);
            WriteLiteral("\r\n        </dt>\r\n        <dd>\r\n            ");
            EndContext();
            BeginContext(2086, 42, false);
#line 79 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Member\Delete.cshtml"
       Write(Html.DisplayFor(model => model.CreateDate));

#line default
#line hidden
            EndContext();
            BeginContext(2128, 38, true);
            WriteLiteral("\r\n        </dd>\r\n    </dl>\r\n    \r\n    ");
            EndContext();
            BeginContext(2166, 207, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "12c2997a39ed4b1f85535197120c4c83", async() => {
                BeginContext(2192, 10, true);
                WriteLiteral("\r\n        ");
                EndContext();
                BeginContext(2202, 36, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "bbd45d2fba754dfda2e10a29befbcaa1", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#line 84 "D:\WORK\GIT\consuls_system\CONSULS_SYSTEM\Views\Member\Delete.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.ID);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(2238, 84, true);
                WriteLiteral("\r\n        <input type=\"submit\" value=\"Delete\" class=\"btn btn-default\" /> |\r\n        ");
                EndContext();
                BeginContext(2322, 38, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f60c1b362eac445ba1f54f02ac93b6c3", async() => {
                    BeginContext(2344, 12, true);
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
                BeginContext(2360, 6, true);
                WriteLiteral("\r\n    ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2373, 10, true);
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CONSULS_SYSTEM.Models.TB_Member> Html { get; private set; }
    }
}
#pragma warning restore 1591
