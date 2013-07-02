function getLogs(){
  $.ajax({
      type: "POST",
      async: false,
      url: "/umbraco/plugins/uTools/uToolsWebService.asmx/GetLogs",
      data: '{"top":"'+$('#maxResults').val()+'","userID":"'+$('.userName').val()+'","logTypeID":"'+$('.logType').val()+'"}',
      contentType: "application/json; charset=utf-8",
      dataType: "json",
      success: function (returnValue){
        var response=returnValue.d;
        //console.log(response);
        
        switch(response.status){
          case 'SUCCESS':
              updateResults(eval(response.logs));
              break;
          case 'ERROR':
              break;
        }
      }
  });
}

function updateResults(logs){
  $resultsDiv=$('#resultsWrapper');
  $table=$resultsDiv.find('table');
  
  $table.find('tr').each(function(index){
    if(index>0) $(this).remove();
  });
  
  if(logs.length>0){
         
    for(var i=0;i<logs.length;i++){
      
      $tr=$("<tr/>");
      $table.append($tr);
      
      $td=$("<td/>");
      $tr.append($td);
      $td.text(logs[i].dateTime);
      
      $td=$("<td/>");
      $tr.append($td);
      $td.text(logs[i].logType);
      
      $td=$("<td/>");
      $tr.append($td);
      $td.text(logs[i].name);
      
      $td=$("<td/>");
      $tr.append($td);
      $td.text(logs[i].nodeID);
      
      $td=$("<td/>");
      $tr.append($td);
      $td.text(logs[i].comment);
    }
  }
  stripe();
}
  
$(function(){
  
  $("#clearResults").click(function(){
    $('#resultsWrapper table tr').each(function(index){
      if(index!=0) $(this).remove();
    });
  });
  
  $("#hardRefresh").click(function(){
    getLogs();
  });
});

