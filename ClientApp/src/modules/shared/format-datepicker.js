"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = function (d, b) {
        extendStatics = Object.setPrototypeOf ||
            ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
            function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
        return extendStatics(d, b);
    };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
Object.defineProperty(exports, "__esModule", { value: true });
var material_1 = require("@angular/material");
var AppDateAdapter = /** @class */ (function (_super) {
    __extends(AppDateAdapter, _super);
    function AppDateAdapter() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.months = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
        return _this;
    }
    AppDateAdapter.prototype.format = function (date, displayFormat) {
        if (displayFormat === 'input') {
            var day = date.getDate().toString();
            day = +day < 10 ? '0' + day : day;
            var month = this.months[date.getMonth()];
            var year = date.getFullYear();
            return day + ' ' + month + ' ' + year;
        }
        return date.toDateString();
    };
    return AppDateAdapter;
}(material_1.NativeDateAdapter));
exports.AppDateAdapter = AppDateAdapter;
exports.APP_DATE_FORMATS = {
    parse: { dateInput: { month: 'shor', year: 'numeric', day: 'numeric' }, },
    display: {
        dateInput: 'input', monthYearLabel: { year: 'numeric', month: 'numeric' },
        dateA11yLabel: { year: 'numeric', month: 'long', day: 'numeric' },
        monthYearA11yLabel: { year: 'numeric', month: 'long' },
    }
};
//# sourceMappingURL=format-datepicker.js.map