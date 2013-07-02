$(function(){
  var $anchors=$('.docTypeDiv, .mediaTypeDiv').hide().parent().find('a');
  
  $anchors.each(function(){
    var $thisA=$(this);
    
    $thisA.html($thisA.parent().find('li').length).click(function(){
      $thisA.parent().parent().find('div').toggle();
    });
  });
  
  $(".editDataType").live('click', function(event){
    
        var $link=$(this);

        UmbClientMgr.openModalWindow(
          '/umbraco/developer/datatypes/editDataType.aspx?id='+$link.attr('rel'), 
          'Edit', 
          true, 
          $(window).width(), 
          $(window).height(),
          0,
          0,
          '',
          function(){}
        );
    });
});