#pragma checksum "D:\Work\STARTUP\consuls_system\CONSULS_SYSTEM\Views\Minutes\Details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "10a5b3ddf8748825a8fb9b848cbf0b0d5a80be63"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Minutes_Details), @"mvc.1.0.view", @"/Views/Minutes/Details.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Minutes/Details.cshtml", typeof(AspNetCore.Views_Minutes_Details))]
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
#line 1 "D:\Work\STARTUP\consuls_system\CONSULS_SYSTEM\Views\_ViewImports.cshtml"
using CONSULS_SYSTEM;

#line default
#line hidden
#line 2 "D:\Work\STARTUP\consuls_system\CONSULS_SYSTEM\Views\_ViewImports.cshtml"
using CONSULS_SYSTEM.Models;

#line default
#line hidden
#line 2 "D:\Work\STARTUP\consuls_system\CONSULS_SYSTEM\Views\Minutes\Details.cshtml"
using CONSULS_SYSTEM.Resources;

#line default
#line hidden
#line 3 "D:\Work\STARTUP\consuls_system\CONSULS_SYSTEM\Views\Minutes\Details.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#line 4 "D:\Work\STARTUP\consuls_system\CONSULS_SYSTEM\Views\Minutes\Details.cshtml"
using Newtonsoft.Json;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"10a5b3ddf8748825a8fb9b848cbf0b0d5a80be63", @"/Views/Minutes/Details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e9f6ffb3c63f73adacf82be85b9e0989819cffe0", @"/Views/_ViewImports.cshtml")]
    public class Views_Minutes_Details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CONSULS_SYSTEM.Models.TB_Minutes>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/layout_admin/dist/css/AdminLTE.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-box-tool"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/layout_admin/Upload-Image/img/demoUpload.jpg"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("alt", new global::Microsoft.AspNetCore.Html.HtmlString(""), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(134, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 7 "D:\Work\STARTUP\consuls_system\CONSULS_SYSTEM\Views\Minutes\Details.cshtml"
  
    ViewData["Title"] = "Details";
    Layout = "~/Views/Shared/_Layout_member.cshtml";

#line default
#line hidden
            BeginContext(269, 69, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "1cbb5065b0d6476ebdfb2cbac6dc2e85", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(338, 1015, true);
            WriteLiteral(@"
<style type=""text/css"">
    /*section {
        margin-top: 3%;
    }*/

    .navbar-custom {
        background-color: black; /*transparent;*/
        /*padding: 25px 0;
        -webkit-transition: padding 0.3s;
        -moz-transition: padding 0.3s;
        transition: padding 0.3s;
        border: none;*/
    }

    .navbar-toggle {
        position: relative;
        float: right;
        padding: 9px 10px;
        margin-top: 8px;
        margin-right: 15px;
        margin-bottom: 8px;
        background-color: transparent;
        background-image: none;
        border: 1px solid transparent;
        border-radius: 4px;
    }
</style>

<br />
<section id=""Minutes"">
    <div class=""container"">
        <div class=""row"">
            <div class=""col-lg-12"">
                <div class=""box box-widget"">
                    <div class=""box-header with-border"">
                        <div class=""user-block"">
                            <h4 class=""section-heading"">");
            EndContext();
            BeginContext(1354, 11, false);
#line 48 "D:\Work\STARTUP\consuls_system\CONSULS_SYSTEM\Views\Minutes\Details.cshtml"
                                                   Write(Model.Title);

#line default
#line hidden
            EndContext();
            BeginContext(1365, 68, true);
            WriteLiteral("</h4>\r\n                            <span class=\"section-subheading\">");
            EndContext();
            BeginContext(1434, 14, false);
#line 49 "D:\Work\STARTUP\consuls_system\CONSULS_SYSTEM\Views\Minutes\Details.cshtml"
                                                        Write(Model.Createby);

#line default
#line hidden
            EndContext();
            BeginContext(1448, 4, true);
            WriteLiteral(" on ");
            EndContext();
            BeginContext(1453, 60, false);
#line 49 "D:\Work\STARTUP\consuls_system\CONSULS_SYSTEM\Views\Minutes\Details.cshtml"
                                                                           Write(Convert.ToDateTime(Model.CreateDate).ToString("MMM dd yyyy"));

#line default
#line hidden
            EndContext();
            BeginContext(1513, 122, true);
            WriteLiteral("</span>\r\n                        </div>\r\n                        <div class=\"box-tools\">\r\n                            <h5>");
            EndContext();
            BeginContext(1635, 155, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d1b327fd6dc4466bb64029bad04e45f0", async() => {
                BeginContext(1682, 53, true);
                WriteLiteral("<i class=\"fa fa-angle-left\" style=\"font-size:15px;\"> ");
                EndContext();
                BeginContext(1736, 46, false);
#line 52 "D:\Work\STARTUP\consuls_system\CONSULS_SYSTEM\Views\Minutes\Details.cshtml"
                                                                                                                               Write(SharedLocalizer.GetLocalizedHtmlString("Back"));

#line default
#line hidden
                EndContext();
                BeginContext(1782, 4, true);
                WriteLiteral("</i>");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(1790, 169, true);
            WriteLiteral("</h5>\r\n                        </div>\r\n                        <!-- /.box-tools -->\r\n                    </div>\r\n                    <div class=\"box-body text-center\">\r\n");
            EndContext();
#line 57 "D:\Work\STARTUP\consuls_system\CONSULS_SYSTEM\Views\Minutes\Details.cshtml"
                         if (Model.Img_Url == null)
                        {

#line default
#line hidden
            BeginContext(2039, 28, true);
            WriteLiteral("                            ");
            EndContext();
            BeginContext(2067, 65, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("img", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "bdd6c9d78a45450d83cfb5f8c14be58c", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_4);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_5);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(2132, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 60 "D:\Work\STARTUP\consuls_system\CONSULS_SYSTEM\Views\Minutes\Details.cshtml"
                        }
                        else
                        {

#line default
#line hidden
            BeginContext(2218, 32, true);
            WriteLiteral("                            <img");
            EndContext();
            BeginWriteAttribute("src", " src=\"", 2250, "\"", 2283, 1);
#line 63 "D:\Work\STARTUP\consuls_system\CONSULS_SYSTEM\Views\Minutes\Details.cshtml"
WriteAttributeValue("", 2256, Url.Content(Model.Img_Url), 2256, 27, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2284, 110, true);
            WriteLiteral(" class=\"img-responsive img-centered\" style=\"border:3px solid #ffffff; border-radius:10px;width:100%\" alt=\"\">\r\n");
            EndContext();
#line 64 "D:\Work\STARTUP\consuls_system\CONSULS_SYSTEM\Views\Minutes\Details.cshtml"
                        }

#line default
#line hidden
            BeginContext(2421, 180, true);
            WriteLiteral("                    </div>\r\n                    <!-- /.box-header -->\r\n                    <div class=\"box-body\">\r\n                        <p class=\"section-subheading text-muted\">");
            EndContext();
            BeginContext(2602, 17, false);
#line 68 "D:\Work\STARTUP\consuls_system\CONSULS_SYSTEM\Views\Minutes\Details.cshtml"
                                                            Write(Model.Description);

#line default
#line hidden
            EndContext();
            BeginContext(2619, 94, true);
            WriteLiteral("</p>\r\n                        <p class=\"section-subheading text-muted\">Document download => <a");
            EndContext();
            BeginWriteAttribute("href", " href=\"", 2713, "\"", 2750, 1);
#line 69 "D:\Work\STARTUP\consuls_system\CONSULS_SYSTEM\Views\Minutes\Details.cshtml"
WriteAttributeValue("", 2720, Url.Content(Model.Attachment), 2720, 30, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(2751, 86, true);
            WriteLiteral(" target=\"_blank\" download=\"\" class=\"header-link\"><i class=\"fa fa-cloud-download\"></i> ");
            EndContext();
            BeginContext(2838, 50, false);
#line 69 "D:\Work\STARTUP\consuls_system\CONSULS_SYSTEM\Views\Minutes\Details.cshtml"
                                                                                                                                                                                                               Write(SharedLocalizer.GetLocalizedHtmlString("Download"));

#line default
#line hidden
            EndContext();
            BeginContext(2888, 124, true);
            WriteLiteral("</a></p>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n\r\n    </div>\r\n</section>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CONSULS_SYSTEM.Models.TB_Minutes> Html { get; private set; }
    }
}
#pragma warning restore 1591
