using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;

namespace Owl.reCAPTCHA.v2.TagHelpers
{
    [HtmlTargetElement("recaptcha-div-v2", TagStructure = TagStructure.WithoutEndTag)]
    public class reCAPTCHAV2DivTagHelper : TagHelper
    {
        public string Badge { get; set; }

        public string Theme { get; set; }

        public string Size { get; set; }

        public string TabIndex { get; set; }

        public string Callback { get; set; }

        public string ExpiredCallback { get; set; }

        public string ErrorCallback { get; set; }

        private readonly reCAPTCHAOptions _options;

        public reCAPTCHAV2DivTagHelper(IOptionsSnapshot<reCAPTCHAOptions> optionsAccessor)
        {
            _options = optionsAccessor.Get(reCAPTCHAConsts.V2);
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            /*
            <div class="g-recaptcha"
               data-sitekey="_your_site_key_"
               data-callback="onSubmit"
               data-size="invisible">
               ....
            </div>
            */

            output.TagName = "div";
            output.TagMode = TagMode.StartTagAndEndTag;

            output.Attributes.Add("class", "g-recaptcha");
            output.Attributes.Add("data-sitekey", _options.SiteKey);
            if (!string.IsNullOrWhiteSpace(Badge))
            {
                output.Attributes.Add("data-badge", Badge);
            }
            if (!string.IsNullOrWhiteSpace(Theme))
            {
                output.Attributes.Add("data-theme", Theme);
            }
            if (!string.IsNullOrWhiteSpace(Size))
            {
                output.Attributes.Add("data-size", Size);
            }
            if (!string.IsNullOrWhiteSpace(TabIndex))
            {
                output.Attributes.Add("data-tabindex", TabIndex);
            }
            if (!string.IsNullOrWhiteSpace(Callback))
            {
                output.Attributes.Add("data-callback", Callback);
            }
            if (!string.IsNullOrWhiteSpace(ExpiredCallback))
            {
                output.Attributes.Add("data-expired-callback", ExpiredCallback);
            }
            if (!string.IsNullOrWhiteSpace(ErrorCallback))
            {
                output.Attributes.Add("data-error-callback", ErrorCallback);
            }
        }
    }
}