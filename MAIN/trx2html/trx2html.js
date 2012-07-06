function summary(){
	var tables = document.getElementsByTagName('table'); 	
	var tSummaryDetail = document.getElementById('tSummaryDetail'); 	
	var globalStats = new Array(); 	var totalTests = 0; 	
	var totalSucceed = 0; 	
	var totalFailed = 0; 	
	var totalIgnored = 0; 	
	var percent = 0; 	
	var totalTime = new Date(0,0,0);
	for (var i=1;i < tSummaryDetail.rows.length;i++){
		var errInfo = parseTestClassResult(tables[i+1]);         
		var testClassPercent = calculatePercent2(errInfo[0], errInfo[1], errInfo[2]);        
        tSummaryDetail.rows[i].cells[1].innerHTML = testClassPercent[0] + '%'; 		
		tSummaryDetail.rows[i].cells[2].innerHTML = createHtmlBars(testClassPercent); 		
		tSummaryDetail.rows[i].cells[3].innerHTML = errInfo[0]; 		
		tSummaryDetail.rows[i].cells[4].innerHTML = errInfo[1]; 		
		tSummaryDetail.rows[i].cells[5].innerHTML = errInfo[2]; 		
		tSummaryDetail.rows[i].cells[6].innerHTML = errInfo[3];		
		globalStats[i-1] = errInfo; 	
	} 	
	for (var i=0;i<globalStats.length;i++){
	    totalTests+=globalStats[i][0]+globalStats[i][1]+globalStats[i][2]; 	    
		totalSucceed+=globalStats[i][0]; 	    
		totalFailed+=globalStats[i][1]; 	    
		totalIgnored+=globalStats[i][2]; 	    
		totalTime = totalTime.addTime(new Date().fromTimeStamp(globalStats[i][3] + '.0')); 	
	} 	
	var tMainSummary = document.getElementById('tMainSummary');	
	percent = calculatePercent2(totalSucceed, totalFailed, totalIgnored); 	
	tMainSummary.rows[1].cells[0].innerHTML= percent[0] + '%'; 	
	tMainSummary.rows[1].cells[1].innerHTML=createHtmlBars(percent); 	
	tMainSummary.rows[1].cells[2].innerHTML=totalTests; 	
	tMainSummary.rows[1].cells[3].innerHTML=totalSucceed; 	
	tMainSummary.rows[1].cells[4].innerHTML=totalFailed; 	
	tMainSummary.rows[1].cells[5].innerHTML=totalIgnored; 	
	tMainSummary.rows[1].cells[6].innerHTML=totalTime.toLongTimeString(); 
} 
function parseTestClassResult(testTable){
	var success = 0; 	
	var failed = 0; 	
	var ignored = 0; 	
	var testClassTime = new Date(0, 0, 0); 
	var t = testTable; 
	for (var i=1;i<t.rows.length;i++){	
	    var testTime = new Date().fromTimeStamp(t.rows[i].cells[3].innerHTML);	  
	    testClassTime = testClassTime.addTime(testTime);		
		var pointEl = t.rows[i].cells[1].children[0]; 		
		switch (pointEl.className){
		    case 'testOk':
		        success++; 		        
				break; 		    
			case 'testKo':
		        failed++; 		        
				break; 		    
			case 'testIgnore':
		        ignored++; 		        
				break; 		
		} 	
	} 	
	return new Array(success, failed, ignored, testClassTime.toLongTimeString()); 
} 
function calculatePercent2(s,f,i){
    var t = s+f+i;     
	var ps=(s/t)*100;
	var pf=(f/t)*100;
	var pi=(i/t)*100; 	
	return new Array(round2(ps), round2(pf), round2(pi) ); 
} 
function round2(v){
    return Math.round(v*100)/100; 
} 				
function createHtmlBars(percent){
    var pcOk = percent[0];     
	var pcKo = percent[1];     
	var pcIg = percent[2]; 	
	var result = '<div class="barContainer">'; 	
	if (pcOk != 0){
		result+='<span class="ok"  style="width:' + pcOk +'%" title="Passed!" >p</span>'; 	
	} 	
	if (pcKo != 0){
		result+='<span class="ko"  style="width:' + pcKo + '%" title="Failed">f</span>'; 	
	} 	
	if (pcIg != 0){
		result+='<span class="ignore"  style="width:' + pcIg+ '%" title="Ignored">i</span>'; 	
	} 	
	result+='</div>'; 	
	return result; 
} 
function togle(anId){
    var el = document.getElementById(anId);     
	if (el!=null){
	    if (el.style.display=='none'){
			el.style.display='block'; 	    
		}else{
			el.style.display='none'; 	    
		}     
	}
} 
Number.prototype.toTSString = function(){
    if (this<10){
		return '0' + this;     
	}else{
		return this;     
	} 
} 
Date.prototype.addTime = function(d){
    return new Date(0,0,0,this.getHours() + d.getHours(),this.getMinutes() + d.getMinutes(),this.getSeconds() + d.getSeconds(),this.getMilliseconds() + d.getMilliseconds()); 
} 
Date.prototype.toLongTimeString = function(){  
	return [this.getHours().toTSString(), this.getMinutes().toTSString(), this.getSeconds().toTSString()].join(":");
} 
Date.prototype.fromTimeStamp= function(ts){
   var t = ts.split(":");     
   var h = t[0];     
   var m = t[1];     
   var s = t[2];     
   var ams = t[2].split(".");     
   return new Date(0, 0, 0, h, m, ams[0], ams[1].substring(0,3)); 
 }