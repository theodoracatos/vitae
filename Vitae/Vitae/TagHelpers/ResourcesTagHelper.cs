using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Reflection;
using System.Dynamic;
using System.Text.Json;
using Library.Resources;

namespace Vitae.TagHelpers
{
    internal class ResourceGroup
    {
        public string Name { get; set; }
        public IEnumerable<LocalizedString> Entries { get; set; }
    }

    [HtmlTargetElement("resources")]
    public class ResourcesTagHelper : TagHelper
    {
        private readonly IStringLocalizer<SharedResource> localizer;

        public ResourcesTagHelper(IStringLocalizer<SharedResource> localizer)
        {
            this.localizer = localizer;
        }

        [HtmlAttributeName("names")]
        public string[] Resources { get; set; }

        /// <summary>
        /// Execute script only once document is loaded.
        /// </summary>
        public bool OnContentLoaded { get; set; } = false;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            if (OnContentLoaded)
                await base.ProcessAsync(context, output);
            else
            {
                IEnumerable<ResourceGroup> groupedResources = Resources.Select(x =>
                {
                    return new ResourceGroup { Name = x, Entries = localizer.GetAllStrings(true).ToList() };
                });

                StringBuilder sb = new StringBuilder();
                sb.Append(GetJavascript(groupedResources));

                TagHelperContent content = await output.GetChildContentAsync();
                sb.Append(content.GetContent());

                output.TagName = "script";
                output.Content.AppendHtml(sb.ToString());
            }
        }

        private string GetJavascript(IEnumerable<ResourceGroup> resources)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("var Resources = ");

            ExpandoObject uiCaptions = new ExpandoObject();

            // Get the fields
            foreach (ResourceGroup fieldGroup in resources)
                ((IDictionary<string, object>)uiCaptions)[fieldGroup.Name] =
                    fieldGroup.Entries.ToDictionary(x => x.Name.ToString(), x => x.Value);

            string serialized = JsonSerializer.Serialize(uiCaptions);
            sb.Append(serialized);
            sb.Append(";");

            return sb.ToString();
        }
    }
}