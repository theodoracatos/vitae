using Microsoft.AspNetCore.Razor.TagHelpers;

using System;

namespace Vitae.TagHelpers
{
    // Triggered on all select elements with the asp-disabled attribute
    [HtmlTargetElement("select", Attributes = DisabledAttributeName)]
    [HtmlTargetElement("button", Attributes = DisabledAttributeName)]
    [HtmlTargetElement("input", Attributes = DisabledAttributeName)]
    public class DisabledTagHelper : TagHelper
    {
        private const string DisabledAttributeName = "asp-disabled";

        /// Get the value of the condition
        [HtmlAttributeName(DisabledAttributeName)]
        public bool Disabled { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (context == null)
                throw new ArgumentNullException(nameof(context));

            if (output == null)
                throw new ArgumentNullException(nameof(output));

            if (Disabled)
                output.Attributes.SetAttribute("disabled", null);
        }
    }
}