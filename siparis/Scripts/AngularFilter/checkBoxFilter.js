var app = angular.module("app", ["checklist-model"]);
app.controller('Ctrl2', function ($scope) {
    $scope.roles = [
      { id: 1, text: 'guest' },
      { id: 2, text: 'user' },
      { id: 3, text: 'customer' },
      { id: 4, text: 'admin' }
    ];
    $scope.user = {
        roles: [2, 4]
    };
    $scope.checkAll = function () {
        $scope.user.roles = $scope.roles.map(function (item) { return item.id; });
    };
    $scope.uncheckAll = function () {
        $scope.user.roles = [];
    };
    $scope.checkFirst = function () {
        $scope.user.roles.splice(0, $scope.user.roles.length);
        $scope.user.roles.push(1);
    };
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

function updateProductList(s, e, f) {
    var group = f.GetSelectedValues();
    angular.element(document.getElementById('Dongu')).scope().getProduct();
}