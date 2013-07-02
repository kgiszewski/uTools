function stripe(){
  $("tr").not(':first').hover(
    function () {
      $(this).css("background","#e6e6e6");
    }, 
    function () {
      $(this).css("background","");
    }
  );
}
    
$(function(){

    $('table').tablesorter({sortList: [[0,0]]});
    stripe();
});