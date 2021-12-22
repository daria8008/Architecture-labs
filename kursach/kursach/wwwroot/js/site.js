
$.ajax({
    type: 'GET',
    dataType: 'json',
    url: "http://bewogek903-001-site1.gtempurl.com/Currency/Data",
    success: function (data) {
        var viewModel = new MainViewModel(data);
        ko.applyBindings(viewModel);
    }.bind(this)
})

function MainViewModel(data) {
    var self = this;

    const firstBankData = data[0];
    self.currencyData = ko.observableArray(data);
    self.selectedBank = ko.observable(firstBankData);

    const CalcBuyOption = "Buy";
    const CalcSaleOption = "Sale";

    self.calcConvertOptions = ko.computed(() => {
        const res = [];
        if (self.selectedBank().data.some(x => x.sale)) {
            res.push(CalcBuyOption);
        }
        if (self.selectedBank().data.some(x => x.buy)) {
            res.push(CalcSaleOption);
        }
        return res;
    });
    self.calcConvertSelectedOption = ko.observable(self.calcConvertOptions()[0]);
    self.exchangeCurr = ko.observable(firstBankData.data[0].currency);
    self.exchangeCurrOptions = firstBankData.data.map(x => x.currency);
    self.exchangeVal1 = ko.observable(0);
    self.exchangeVal2 = ko.observable(0);

    self.exchangeValUpdateStarted = false;

    self.exchangeVal1.subscribe((newValue) => {

        if (self.exchangeValUpdateStarted) {
            self.exchangeValUpdateStarted = false;
            return;
        } else {
            self.exchangeValUpdateStarted = true;
        }

        const currencyInfo = self.selectedBank().data.find(x => x.currency === self.exchangeCurr());
        if (self.calcConvertSelectedOption() === CalcBuyOption) {
            self.exchangeVal2(parseFloat(newValue) / currencyInfo.sale);
        }
        if (self.calcConvertSelectedOption() === CalcSaleOption) {
            self.exchangeVal2(parseFloat(newValue) * currencyInfo.buy);
        }
    })

    self.exchangeVal2.subscribe((newValue) => {

        if (self.exchangeValUpdateStarted) {
            self.exchangeValUpdateStarted = false;
            return;
        } else {
            self.exchangeValUpdateStarted = true;
        }

        const currencyInfo = self.selectedBank().data.find(x => x.currency === self.exchangeCurr());
        if (self.calcConvertSelectedOption() === CalcBuyOption) {
            self.exchangeVal1(parseFloat(newValue) * currencyInfo.sale);
        }
        if (self.calcConvertSelectedOption() === CalcSaleOption) {
            self.exchangeVal1(parseFloat(newValue) / currencyInfo.buy);
        }
    })

    self.calcConvertSelectedOption.subscribe((newValue) => {

        if (newValue === CalcBuyOption) {
            self.exchangeVal2(self.exchangeVal1());
        }
        if (newValue === CalcSaleOption) {
            self.exchangeVal1(self.exchangeVal2());
        }
    })

    self.exchangeCurr.subscribe((newValue) => {
        self.exchangeVal1(0);
        self.exchangeVal2(0);
    })

    self.toggleActive = function (tab) {
        self.selectedBank(tab);
        self.exchangeVal1(0);
        self.exchangeVal2(0);
    }
}