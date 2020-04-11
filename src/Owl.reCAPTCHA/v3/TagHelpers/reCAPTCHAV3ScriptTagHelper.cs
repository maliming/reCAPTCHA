using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;

namespace Owl.reCAPTCHA.v3.TagHelpers
{
    [HtmlTargetElement("recaptcha-script-v3", TagStructure = TagStructure.WithoutEndTag)]
    public class reCAPTCHAV3ScriptTagHelper : TagHelper
    {
        private readonly reCAPTCHAOptions _options;

        private readonly IreCAPTCHALanguageCodeProvider _reCAPTCHALanguageCodeProvider;

        public reCAPTCHAV3ScriptTagHelper(IOptionsSnapshot<reCAPTCHAOptions> optionsAccessor, 
            IreCAPTCHALanguageCodeProvider reCaptchaLanguageCodeProvider)
        {
            _options = optionsAccessor.Get(reCAPTCHAConsts.V3);
            _reCAPTCHALanguageCodeProvider = reCaptchaLanguageCodeProvider;
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            /*
                <script src="https://www.google.com/recaptcha/api.js?render=_reCAPTCHA_site_key"></script>
            */

            output.TagName = "script";
            output.TagMode = TagMode.StartTagAndEndTag;

            var src = $"{_options.VerifyBaseUrl.RemovePostFix(StringComparison.OrdinalIgnoreCase, "/")}/recaptcha/api.js?hl={_reCAPTCHALanguageCodeProvider.GetLanguageCode()}&render={_options.SiteKey}";

            output.Attributes.Add(new TagHelperAttribute("src", new HtmlString(src)));
        }
    }
}