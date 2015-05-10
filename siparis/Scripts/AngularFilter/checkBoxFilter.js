var rootApp = angular.module('rootApp', ['firstApp','urunApp']);

var firstApp = angular.module('firstApp', []);
    firstApp.controller('FirstController', function ($scope) {
        $scope.desc = "First app.";
    });

var urunApp = angular.module("urunApp", []);
urunApp.controller('Dongu', ['$scope', '$http', function ($scope, $http) {
    var onComplete = function (response) {
        $scope.urunler = response.data;


    };

    var onError = function (response) {
        $scope.error = 'Github kullanıcısı bulunamadı!';
    };
    $scope.getProduct = function () {
        var cat = $('input[name=CATEGORY]');
        var msj = {
            stokMainGroup: $('#stokMainGroup').val(),
            stokSubGroup: $('#stokSubGroup').val(),
            stokSubGroup2: $('#stokSubGroup2').val(),
            stokCategory: CATEGORY.GetSelectedValues(),
            stokbrand: BRAND.GetSelectedValues(),
            stokseason: SEASON.GetSelectedValues(),
            stokcolor: COLOR.GetSelectedValues(),
            stokBody: SIZE.GetSelectedValues(),
            stokPacket: PACKET.GetSelectedValues(),
            stokRayon: RAYON.GetSelectedValues(),
            stokModel: MODEL.GetSelectedValues(),
            stokSector: SECTOR.GetSelectedValues()
        };
        $http.post('FilterProduct', msj)
         .then(onComplete, onError);
    }
}]);

function updateProductList() {

    // CATEGORY.Enabled = false;
    angular.element(document.getElementById('Dongu')).scope().getProduct();
}
