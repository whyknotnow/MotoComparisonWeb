$(document).ready(function () {
    function setupAutocomplete(brandSelector, modelSelector, hiddenInput) {
        $(brandSelector).change(function () {
            $(modelSelector).val('').data('manufacturer-url', $(this).val());
        });

        $(modelSelector).on('input', function () {
            var manufacturerUrl = $(this).data('manufacturer-url');
            var term = $(this).val();
            if (manufacturerUrl && term.length >= 2) {
                $.get("/Motorcycle/GetModelSuggestions", { manufacturerUrl: manufacturerUrl, term: term }, function (data) {
                    var suggestionsBox = $(modelSelector).siblings('.suggestions');
                    suggestionsBox.html(data).show();
                    suggestionsBox.find('li').click(function () {
                        $(modelSelector).val($(this).text());
                        $(hiddenInput).val($(this).data('value'));
                        suggestionsBox.hide();
                    });
                });
            } else {
                $(modelSelector).siblings('.suggestions').hide();
            }
        });

        $(document).click(function (e) {
            if (!$(e.target).closest(modelSelector).length && !$(e.target).closest('.suggestions').length) {
                $(modelSelector).siblings('.suggestions').hide();
            }
        });
    }

    setupAutocomplete("#brand1", "#model1", "#modelUrl1");
    setupAutocomplete("#brand2", "#model2", "#modelUrl2");
});
