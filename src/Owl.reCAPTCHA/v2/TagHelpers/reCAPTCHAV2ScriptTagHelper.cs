using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;

namespace Owl.reCAPTCHA.v2.TagHelpers;

[HtmlTargetElement("recaptcha-script-v2", TagStructure = TagStructure.WithoutEndTag)]
public class reCAPTCHAV2ScriptTagHelper : TagHelper
{
    public bool ScriptAsync { get; set; } = true;

    public bool ScriptDefer { get; set; } = true;

    public string Onload { get; set; }

    public string Render { get; set; }

    public bool HideBadge { get; set; }

    private readonly reCAPTCHAOptions _options;

    private readonly IreCAPTCHALanguageCodeProvider _reCAPTCHALanguageCodeProvider;

    public reCAPTCHAV2ScriptTagHelper(IOptionsSnapshot<reCAPTCHAOptions> optionsAccessor,
        IreCAPTCHALanguageCodeProvider reCaptchaLanguageCodeProvider)
    {
        _options = optionsAccessor.Get(reCAPTCHAConsts.V2);
        _reCAPTCHALanguageCodeProvider = reCaptchaLanguageCodeProvider;
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        /*
            <script src="https://www.google.com/recaptcha/api.js" async defer></script>
        */

        output.TagName = "";

        var src = $"{_options.VerifyBaseUrl.RemovePostFix(StringComparison.OrdinalIgnoreCase, "/")}/recaptcha/api.js?" +
                  $"hl={_reCAPTCHALanguageCodeProvider.GetLanguageCode()}";
        if (!string.IsNullOrWhiteSpace(Onload))
        {
            src += $"&onload={Onload}";
        }
        if (!string.IsNullOrWhiteSpace(Render))
        {
            src += $"&render={Render}";
        }

        var scriptAsync = ScriptAsync ? "async" : string.Empty;
        var scriptDefer = ScriptDefer ? "defer" : string.Empty;

        output.Content.SetHtmlContent($"<script {scriptAsync} {scriptDefer} src=\"{src}\"></script>");

        if (HideBadge)
        {
            output.PostElement.SetHtmlContent("<style>.grecaptcha-badge{visibility:hidden;}</style>");
        }
    }
}
