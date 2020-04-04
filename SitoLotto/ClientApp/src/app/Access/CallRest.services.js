"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var CallRest = /** @class */ (function () {
    function CallRest() {
    }
    CallRest.Lotto_Get = CallRest_Lotto_Get;
    return CallRest;
}());
exports.CallRest = CallRest;
function CallRest_Lotto_Get(params) {
    return this.httpClient.request('GET', this.heroesUrl, { responseType: 'json', params: params });
}
//# sourceMappingURL=CallRest.js.map