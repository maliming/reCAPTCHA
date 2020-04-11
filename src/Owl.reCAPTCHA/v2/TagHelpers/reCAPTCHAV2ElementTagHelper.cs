using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;

namespace Owl.reCAPTCHA.v2.TagHelpers
{
    [HtmlTargetElement("*", Attributes = BadgeAttributeName)]
    [HtmlTargetElement("*", Attributes = ThemeAttributeName)]
    [HtmlTargetElement("*", Attributes = SizeAttributeName)]
    [HtmlTargetElement("*", Attributes = TabIndexAttributeName)]
    [HtmlTargetElement("*", Attributes = CallbackAttributeName)]
    [HtmlTargetElement("*", Attributes = ExpiredCallbackAttributeName)]
    [HtmlTargetElement("*", Attributes = ErrorCallbackAttributeName)]
    public class reCAPTCHAV2ElementTagHelper : TagHelper
    {
        private const string BadgeAttributeName = "recaptcha-v2-badge";
        private const string ThemeAttributeName = "recaptcha-v2-theme";
        private const string SizeAttributeName = "recaptcha-v2-size";
        private const string TabIndexAttributeName = "recaptcha-v2-tab-index";
        private const string CallbackAttributeName = "recaptcha-v2-callback";
        private const string ExpiredCallbackAttributeName = "recaptcha-v2-expired-callback";
        private const string ErrorCallbackAttributeName = "recaptcha-v2-error-callback";

        [HtmlAttributeName(BadgeAttributeName)]
        public string Badge { get; set; }

        [HtmlAttributeName(ThemeAttributeName)]
        public string Theme { get; set; }

        [HtmlAttributeName(SizeAttributeName)]
        public string Size { get; set; }

        [HtmlAttributeName(TabIndexAttributeName)]
        public string TabIndex { get; set; }

        [HtmlAttributeName(CallbackAttributeName)]
        public string Callback { get; set; }

        [HtmlAttributeName(ExpiredCallbackAttributeName)]
        public string ExpiredCallback { get; set; }

        [HtmlAttributeName(ErrorCallbackAttributeName)]
        public string ErrorCallback { get; set; }

        private readonly reCAPTCHAOptions _options;

        public reCAPTCHAV2ElementTagHelper(IOptionsSnapshot<reCAPTCHAOptions> optionsAccessor)
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