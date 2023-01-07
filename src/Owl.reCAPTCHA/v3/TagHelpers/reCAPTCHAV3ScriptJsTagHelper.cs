using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Options;

namespace Owl.reCAPTCHA.v3.TagHelpers;

[HtmlTargetElement("recaptcha-script-v3-js", TagStructure = TagStructure.WithoutEndTag)]
public class reCAPTCHAV3ScriptJsTagHelper : TagHelper
{
    public string Action { get; set; }

    public string Callback { get; set; }

    public bool Execute { get; set; }

    private readonly reCAPTCHAOptions _options;

    public reCAPTCHAV3ScriptJsTagHelper(IOptionsSnapshot<reCAPTCHAOptions> optionsAccessor)
    {
        _options = optionsAccessor.Get(reCAPTCHAConsts.V3);
        Execute = true;
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        /*
        myCallback is a user-defined method name or `(function(t){alert(t)})` when Execute = true
        grecaptcha.ready(function () {
            grecaptcha.reExecute = function () {
                grecaptcha.execute('6LccrsMUAAAAANSAh_MCplqdS9AJVPihyzmbPqWa', {
                    action: 'login'
                }).then(function (token) {
                    myCallback(token)
                })
            };
            grecaptcha.reExecute()
        });

        myCallback is a user-defined function when Execute = false
        grecaptcha.ready(function () {
            grecaptcha.reExecute = function (callback) {
                grecaptcha.execute('6LccrsMUAAAAANSAh_MCplqdS9AJVPihyzmbPqWa', {
                    action: 'login'
                }).then(myCallback)
            };
        });
         */
        output.TagName = "script";
        output.TagMode = TagMode.StartTagAndEndTag;

        var script =
            "grecaptcha.ready(function(){ " +
            "grecaptcha.reExecute = function(" + (Execute ? "" : "callback") + "){" +
            "grecaptcha.execute('" + _options.SiteKey + "'" + (string.IsNullOrWhiteSpace(Action) ? "" : ",{action:'" + Action + "'}") + ")" +
            (Execute ? (".then(function(token){" + Callback + "(token)" + "})") : ".then(callback)") + "};" +
            (Execute ? "grecaptcha.reExecute()" :"") +
            "});";
        output.Content.SetHtmlContent(script);
    }
}