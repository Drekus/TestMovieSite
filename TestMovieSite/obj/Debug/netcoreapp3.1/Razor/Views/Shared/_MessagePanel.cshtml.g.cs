#pragma checksum "D:\Programing\TestMovieSite\TestMovieSite-master\TestMovieSite-master\TestMovieSite\Views\Shared\_MessagePanel.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1c1ea2c30456d402bade91acb8f42fdad7963013"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__MessagePanel), @"mvc.1.0.view", @"/Views/Shared/_MessagePanel.cshtml")]
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
#nullable restore
#line 1 "D:\Programing\TestMovieSite\TestMovieSite-master\TestMovieSite-master\TestMovieSite\Views\_ViewImports.cshtml"
using TestMovieSite;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"1c1ea2c30456d402bade91acb8f42fdad7963013", @"/Views/Shared/_MessagePanel.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ea525e72b7f24f2a2f95cbac6bfd8832a08003ed", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__MessagePanel : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<TestMovieSite.Views.ViewModels.BaseViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 3 "D:\Programing\TestMovieSite\TestMovieSite-master\TestMovieSite-master\TestMovieSite\Views\Shared\_MessagePanel.cshtml"
 if (Model.ShowMessage)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <br />\n    <div class=\"col-md-offset-2 col-md-8\">\n        <div");
            BeginWriteAttribute("class", " class=\"", 145, "\"", 213, 3);
            WriteAttributeValue("", 153, "panel", 153, 5, true);
            WriteAttributeValue(" ", 158, "panel-", 159, 7, true);
#nullable restore
#line 7 "D:\Programing\TestMovieSite\TestMovieSite-master\TestMovieSite-master\TestMovieSite\Views\Shared\_MessagePanel.cshtml"
WriteAttributeValue("", 165, Model.IsSuccessMessage ? "success" : "danger", 165, 48, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">\n            <div class=\"panel-body\">\n                <h3");
            BeginWriteAttribute("class", " class=\"", 272, "\"", 345, 3);
            WriteAttributeValue("", 280, "text-center", 280, 11, true);
            WriteAttributeValue(" ", 291, "text-", 292, 6, true);
#nullable restore
#line 9 "D:\Programing\TestMovieSite\TestMovieSite-master\TestMovieSite-master\TestMovieSite\Views\Shared\_MessagePanel.cshtml"
WriteAttributeValue("", 297, Model.IsSuccessMessage ? "success" : "danger", 297, 48, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">");
#nullable restore
#line 9 "D:\Programing\TestMovieSite\TestMovieSite-master\TestMovieSite-master\TestMovieSite\Views\Shared\_MessagePanel.cshtml"
                                                                                          Write(Model.IsSuccessMessage ? "Success" : "Error");

#line default
#line hidden
#nullable disable
            WriteLiteral("! ");
#nullable restore
#line 9 "D:\Programing\TestMovieSite\TestMovieSite-master\TestMovieSite-master\TestMovieSite\Views\Shared\_MessagePanel.cshtml"
                                                                                                                                          Write(Model.Message);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\n            </div>\n        </div>\n    </div>\n    <br />\n");
#nullable restore
#line 14 "D:\Programing\TestMovieSite\TestMovieSite-master\TestMovieSite-master\TestMovieSite\Views\Shared\_MessagePanel.cshtml"
}

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<TestMovieSite.Views.ViewModels.BaseViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
