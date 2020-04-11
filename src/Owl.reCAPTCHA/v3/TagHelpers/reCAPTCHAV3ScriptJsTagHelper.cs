using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;

namespace Owl.reCAPTCHA.v3.TagHelpers
{
    [HtmlTargetElement("recaptcha-script-v3-js", TagStructure = TagStructure.WithoutEndTag)]
    public class reCAPTCHAV3ScriptJsTagHelper : TagHelper
    {
        public string Action { get; set; }

        public string Callback { get; set; }

        private readonly reCAPTCHAOptions _options;

        public reCAPTCHAV3ScriptJsTagHelper(IOptionsSnapshot<reCAPTCHAOptions> optionsAccessor)
        {
            _options = optionsAccessor.Get(reCAPTCHAConsts.V3);
        }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            /*
            grecaptcha.ready(function() {
                grecaptcha.reExecute = function(){
                    grecaptcha.execute('_reCAPTCHA_site_key_', {action: 'homepage'}).then(function(token) {
                   ...
                })();
                }
            });
            */

            output.TagName = "script";
            output.TagMode = TagMode.StartTagAndEndTag;

            var script = "grecaptcha.ready(function(){ " +
                         "grecaptcha.reExecute = function(){" +
                             "grecaptcha.execute('" + _options.SiteKey + "'" + (string.IsNullOrWhiteSpace(Action) ? "" : ",{action:'" + Action + "'}") + ")" +
                                 ".then(function(token){" +
                                    Callback + "(token)" +
                                 "})" +
                            "};" +
                           "grecaptcha.reExecute()" +
                         "});";
            output.Content.SetHtmlContent(script);
        }
    }
}