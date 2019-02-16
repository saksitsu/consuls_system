/* BoxRefresh()
 * =========
 * Adds AJAX content control to a box.
 *
 * @Usage: $('#my-box').boxRefresh(Options)
 *         or add [data-widget="box-refresh"] to the box element
 *         Pass any Option as data-Option="value"
 */
+function ($) {
  'use strict';

  var DataKey = 'lte.boxrefresh';

  var Default = {
    source         : '',
    params         : {},
    trigger        : '.refresh-btn',
    content        : '.box-body',
    loadInContent  : true,
    responseType   : '',
    overlayTemplate: '<div class="overlay"><div class="fa fa-refresh fa-spin"></div></div>',
    onLoadStart    : function () {
    },
    onLoadDone     : function (response) {
      return response;
    }
  };

  var Selector = {
    data: '[data-widget="box-refresh"]'
  };

  // BoxRefresh Class Definition
  // =========================
  var BoxRefresh = function (element, Options) {
    this.element  = element;
    this.Options  = Options;
    this.$overlay = $(Options.overlay);

    if (Options.source === '') {
      throw new Error('Source url was not defined. Please specify a url in your BoxRefresh source Option.');
    }

    this._setUpListeners();
    this.load();
  };

  BoxRefresh.prototype.load = function () {
    this._addOverlay();
    this.Options.onLoadStart.call($(this));

    $.get(this.Options.source, this.Options.params, function (response) {
      if (this.Options.loadInContent) {
        $(this.Options.content).html(response);
      }
      this.Options.onLoadDone.call($(this), response);
      this._removeOverlay();
    }.bind(this), this.Options.responseType !== '' && this.Options.responseType);
  };

  // Private

  BoxRefresh.prototype._setUpListeners = function () {
    $(this.element).on('click', Selector.trigger, function (event) {
      if (event) event.preventDefault();
      this.load();
    }.bind(this));
  };

  BoxRefresh.prototype._addOverlay = function () {
    $(this.element).append(this.$overlay);
  };

  BoxRefresh.prototype._removeOverlay = function () {
    $(this.element).remove(this.$overlay);
  };

  // Plugin Definition
  // =================
  function Plugin(Option) {
    return this.each(function () {
      var $this = $(this);
      var data  = $this.data(DataKey);

      if (!data) {
        var Options = $.extend({}, Default, $this.data(), typeof Option == 'object' && Option);
        $this.data(DataKey, (data = new BoxRefresh($this, Options)));
      }

      if (typeof data == 'string') {
        if (typeof data[Option] == 'undefined') {
          throw new Error('No method named ' + Option);
        }
        data[Option]();
      }
    });
  }

  var old = $.fn.boxRefresh;

  $.fn.boxRefresh             = Plugin;
  $.fn.boxRefresh.Constructor = BoxRefresh;

  // No Conflict Mode
  // ================
  $.fn.boxRefresh.noConflict = function () {
    $.fn.boxRefresh = old;
    return this;
  };

  // BoxRefresh Data API
  // =================
  $(window).on('load', function () {
    $(Selector.data).each(function () {
      Plugin.call($(this));
    });
  });

}(jQuery);
