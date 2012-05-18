EntityList = function (list) {
     var thisList = this;
     var innerList = [];
     if ($.isArray(list))
         innerList = list;

     var getIndexById = function (id) {
         var index;
         $.grep(innerList, function (item, ix) {
             if (item && item.Id == id) {
                 index = ix;
                 return true;
             }
             return false;
         });

         return index;
     };

     this.getById = function (id) {
         var index = getIndexById(id);
         if (index >= 0) {
             return innerList[index];
         }
         return null;
     };

     this.find = function (property, value) {
         var result = $.grep(innerList, function (itm) {
             return itm[property] == value;
         });
         return result;
     };


     this.deleteById = function (id) {
         var index = getIndexById(id);
         if (index >= 0) {
             entityRemoved();
             innerList.splice(index, 1);
             return true;
         }
         return false;
     };

     this.push = function (element) {
         entityAdded();
         return innerList.push(element);
     };

     this.insert = function (item, index) {
         if (typeof index == "undefined" || isNaN(index)) {
             innerList.push(item);
         } else {
             innerList.splice(index, 0, item);
         }
         entityAdded();
     };

     this.toArray = function () {
         return innerList;
     };

     var entityAdded = function () {
         $(thisList).trigger("entityAdded");
     };
     
     var entityRemoved = function () {
         $(thisList).trigger("entityRemoved");
     };
 };