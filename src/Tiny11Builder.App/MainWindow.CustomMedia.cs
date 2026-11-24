using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Tiny11Builder.Core;

namespace Tiny11Builder.App;

public sealed partial class MainWindow
{
    private long? recipeIsoSizeLimit;
    private bool ExpertAccepted => ExpertConsent.IsChecked == true || NativePayloadConsent.IsChecked == true;
    // Nano11 selects the direct payload groups as part of its preset, so its agreement dialog replaces the
    // advanced-risk checkbox. Custom still requires the acknowledgement for the same groups.
    private bool RequiresExpertAcknowledgement => SelectedMode != BuildMode.Nano11 &&
        (options.Any(option => option.Selected && option.Option.CustomOnly) ||
        (inventory.Count > 0 ? inventory.Any(item => item.Selected) : pendingActions.Length > 0));

    private static string CustomMediaSummary(BuildRecipe recipe) =>
        (recipe.ShrinkBootImage ? UiText.Get("CustomMedia.ShrinkBoot") + "\n" : "") +
        (recipe.IsoSizeLimitBytes is { } limit ? UiText.Format("CustomMedia.ReviewLimit", limit) : "");

    private void OnClearRecipeLimit(object sender, RoutedEventArgs args)
    {
        if (busy || modalOpen) return;
        recipeIsoSizeLimit = null;
        UpdateCounts();
    }
}
