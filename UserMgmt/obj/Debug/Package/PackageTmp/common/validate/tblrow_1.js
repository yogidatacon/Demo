/* 
 * To change this license header, choose License Headers in Project Properties.
 * To change this template file, choose Tools | Templates
 * and open the template in the editor.
 */


function reomoveTblRowBtcForm17(row,tblnm,rowcls){
                var rowCount = $('#'+tblnm+' tr:visible').length;
                if (rowCount !== 2) {
        $(row).parents('.'+rowcls).remove();
                return true;
} else {
                alert('You cannot delete this row');
                return false;
}

}