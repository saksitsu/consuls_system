#pragma checksum "D:\Work\STARTUP\consuls_system\CONSULS_SYSTEM\Views\Shared\_MyAlert.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7c877d8b5b0df77a277cc0a3bf086007420020a0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__MyAlert), @"mvc.1.0.view", @"/Views/Shared/_MyAlert.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Shared/_MyAlert.cshtml", typeof(AspNetCore.Views_Shared__MyAlert))]
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
#line 1 "D:\Work\STARTUP\consuls_system\CONSULS_SYSTEM\Views\Shared\_MyAlert.cshtml"
using CONSULS_SYSTEM.Resources;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7c877d8b5b0df77a277cc0a3bf086007420020a0", @"/Views/Shared/_MyAlert.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e9f6ffb3c63f73adacf82be85b9e0989819cffe0", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__MyAlert : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(69, 2, true);
            WriteLiteral("\r\n");
            EndContext();
#line 4 "D:\Work\STARTUP\consuls_system\CONSULS_SYSTEM\Views\Shared\_MyAlert.cshtml"
 if (ViewBag.MyAlert != null)
{

#line default
#line hidden
            BeginContext(105, 109, true);
            WriteLiteral("    <script src=\"https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js\"></script>\r\n    <a href=\"#\"");
            EndContext();
            BeginWriteAttribute("onclick", " onclick=\"", 214, "\"", 299, 3);
            WriteAttributeValue("", 224, "clickme3(", 224, 9, true);
#line 7 "D:\Work\STARTUP\consuls_system\CONSULS_SYSTEM\Views\Shared\_MyAlert.cshtml"
WriteAttributeValue("", 233, SharedLocalizer.GetLocalizedHtmlString(@ViewBag.MyAlert.Message), 233, 65, false);

#line default
#line hidden
            WriteAttributeValue("", 298, ")", 298, 1, true);
            EndWriteAttribute();
            BeginContext(300, 203, true);
            WriteLiteral(" id=\"MyAlert\"></a>\r\n    <script type=\"text/javascript\">\r\n\r\n        $(function () {\r\n            NotificationCheck();\r\n        });\r\n\r\n        function NotificationCheck() {\r\n                var Status = \'");
            EndContext();
            BeginContext(504, 22, false);
#line 15 "D:\Work\STARTUP\consuls_system\CONSULS_SYSTEM\Views\Shared\_MyAlert.cshtml"
                         Write(ViewBag.MyAlert.Status);

#line default
#line hidden
            EndContext();
            BeginContext(526, 74, true);
            WriteLiteral("\';\r\n\r\n            if (Status == \"true\") {\r\n                click_success(\'");
            EndContext();
            BeginContext(601, 64, false);
#line 18 "D:\Work\STARTUP\consuls_system\CONSULS_SYSTEM\Views\Shared\_MyAlert.cshtml"
                          Write(SharedLocalizer.GetLocalizedHtmlString(@ViewBag.MyAlert.Message));

#line default
#line hidden
            EndContext();
            BeginContext(665, 79, true);
            WriteLiteral("\');\r\n            } else if (Status == \"false\") {\r\n                click_error(\'");
            EndContext();
            BeginContext(745, 64, false);
#line 20 "D:\Work\STARTUP\consuls_system\CONSULS_SYSTEM\Views\Shared\_MyAlert.cshtml"
                        Write(SharedLocalizer.GetLocalizedHtmlString(@ViewBag.MyAlert.Message));

#line default
#line hidden
            EndContext();
            BeginContext(809, 83, true);
            WriteLiteral("\');\r\n            } else if (Status == \"message\") {\r\n                click_message(\'");
            EndContext();
            BeginContext(893, 64, false);
#line 22 "D:\Work\STARTUP\consuls_system\CONSULS_SYSTEM\Views\Shared\_MyAlert.cshtml"
                          Write(SharedLocalizer.GetLocalizedHtmlString(@ViewBag.MyAlert.Message));

#line default
#line hidden
            EndContext();
            BeginContext(957, 923, true);
            WriteLiteral(@"');
            }
        }

    function click_confirm() {
        swal({
            title: ""Are you sure?"",
            text: ""You will not be able to recover this imaginary file!"",
            type: ""warning"",
            showCancelButton: true,
            confirmButtonClass: ""btn-danger"",
            confirmButtonText: ""Yes, delete it!"",
            cancelButtonText: ""No, cancel plx!"",
            closeOnConfirm: false,
            closeOnCancel: false
        },
            function (isConfirm) {
                if (isConfirm) {
                    $('#btnSave').trigger('click');
                    swal(""Deleted!"", ""Your imaginary file has been deleted."", ""success"");
                } else {
                    swal(""Cancelled"", ""Your imaginary file is safe :)"", ""error"");
                }
            });
    }

    function click_error(message) {
        swal(
            '");
            EndContext();
            BeginContext(1881, 47, false);
#line 50 "D:\Work\STARTUP\consuls_system\CONSULS_SYSTEM\Views\Shared\_MyAlert.cshtml"
        Write(SharedLocalizer.GetLocalizedHtmlString("Error"));

#line default
#line hidden
            EndContext();
            BeginContext(1928, 134, true);
            WriteLiteral("\',\r\n            message,\r\n            \'error\'\r\n        )\r\n    }\r\n\r\n    function click_success(message) {\r\n        swal(\r\n            \'");
            EndContext();
            BeginContext(2063, 49, false);
#line 58 "D:\Work\STARTUP\consuls_system\CONSULS_SYSTEM\Views\Shared\_MyAlert.cshtml"
        Write(SharedLocalizer.GetLocalizedHtmlString("Success"));

#line default
#line hidden
            EndContext();
            BeginContext(2112, 151, true);
            WriteLiteral("\',\r\n            message,\r\n            \'success\'\r\n        )\r\n    }\r\n    function click_message(message) {\r\n        swal(message)\r\n    }\r\n    </script>\r\n");
            EndContext();
#line 67 "D:\Work\STARTUP\consuls_system\CONSULS_SYSTEM\Views\Shared\_MyAlert.cshtml"
}

#line default
#line hidden
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
