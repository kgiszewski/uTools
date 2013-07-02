$(function(){
  $(".editDocType").live('click', function(event){
    
        var $link=$(this);

        UmbClientMgr.openModalWindow(
          '/umbraco/settings/editNodeTypeNew.aspx?id='+$link.attr('rel'), 
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