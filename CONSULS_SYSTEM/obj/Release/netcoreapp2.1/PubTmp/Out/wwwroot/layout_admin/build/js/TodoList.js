/* TodoList()
 * =========
 * Converts a list into a todoList.
 *
 * @Usage: $('.my-list').todoList(Options)
 *         or add [data-widget="todo-list"] to the ul element
 *         Pass any Option as data-Option="value"
 */
+function ($) {
  'use strict';

  var DataKey = 'lte.todolist';

  var Default = {
    onCheck  : function (item) {
      return item;
    },
    onUnCheck: function (item) {
      return item;
    }
  };

  var Selector = {
    data: '[data-widget="todo-list"]'
  };

  var ClassName = {
    done: 'done'
  };

  // TodoList Class Definition
  // =========================
  var TodoList = function (element, Options) {
    this.element = element;
    this.Options = Options;

    this._setUpListeners();
  };

  TodoList.prototype.toggle = function (item) {
    item.parents(Selector.li).first().toggleClass(ClassName.done);
    if (!item.prop('checked')) {
      this.unCheck(item);
      return;
    }

    this.check(item);
  };

  TodoList.prototype.check = function (item) {
    this.Options.onCheck.call(item);
  };

  TodoList.prototype.unCheck = function (item) {
    this.Options.onUnCheck.call(item);
  };

  // Private

  TodoList.prototype._setUpListeners = function () {
    var that = this;
    $(this.element).on('change ifChanged', 'input:checkbox', function () {
      that.toggle($(this));
    });
  };

  // Plugin Definition
  // =================
  function Plugin(Option) {
    return this.each(function () {
      var $this = $(this);
      var data  = $this.data(DataKey);

      if (!data) {
        var Options = $.extend({}, Default, $this.data(), typeof Option == 'object' && Option);
        $this.data(DataKey, (data = new TodoList($this, Options)));
      }

      if (typeof data == 'string') {
        if (typeof data[Option] == 'undefined') {
          throw new Error('No method named ' + Option);
        }
        data[Option]();
      }
    });
  }

  var old = $.fn.todoList;

  $.fn.todoList             = Plugin;
  $.fn.todoList.Constructor = TodoList;

  // No Conflict Mode
  // ================
  $.fn.todoList.noConflict = function () {
    $.fn.todoList = old;
    return this;
  };

  // TodoList Data API
  // =================
  $(window).on('load', function () {
    $(Selector.data).each(function () {
      Plugin.call($(this));
    });
  });

}(jQuery);
