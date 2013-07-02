$(function(){
    $(".editTemplate").live('click', function(event){
    
        var $link=$(this);

        UmbClientMgr.openModalWindow(
          '/umbraco/settings/editTemplate.aspx?templateID='+$link.attr('rel'), 
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