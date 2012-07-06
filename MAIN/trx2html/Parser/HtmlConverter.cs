using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using trx2html;
namespace trx2html.Parser
{
	class HtmlConverter
	{
		TestRunResult run;
		public HtmlConverter(TestRunResult testRunResult)
		{
			run = testRunResult;
		}
		public string GetHtml()
		{
			StringBuilder result = new StringBuilder();
			WriteHeader(run, result);
			WriteBody(run, result);
			WriteEnvironmentInfo(run, result);
			WriteFooter(run, result);
			return result.ToString();
		}

		

		private void WriteHeader(TestRunResult run, StringBuilder result)
		{
			result.AppendFormat("<html><head><title>{0}</title>", run.Name);
			//result.Append("<link type='text/css' rel='StyleSheet'  href='trx2html.css'   />");
			result.Append("<style type='text/css'>");
			result.Append(GetCss());            
			result.Append("</style>");
			result.Append("<script type=\"text/javascript\">");
			result.Append(@"
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

						function showAllTestClassesSummary(){
							var tSummaryDetails = document.getElementById('tSummaryDetail');
							for (var i=0;i<tSummaryDetails.rows.length;i++){
								r = tSummaryDetails.rows[i];
								r.style.display='block';
							}
						}

						");
			result.Append("</script>");
			result.Append("</head><body>");
			result.Append("<a name='__top' />");
			result.AppendFormat(@"<h3>{0}</h3>
								<div class='contents'>
								  <a href='#totals'>Totals</a>
								  |
								  <a href='#summary'>Summary</a>
								  |
								  <a href='#slower'>Slowers</a>
								  |
								  <a href='#detail'>Detail</a>
								  |
								  <a href='#envInfo'>Environment Information</a>
								</div>
								<br />
								<a name='totals' />",
								run.Name);
		}

		private string GetCss()
		{
			return @"
				body, td, th {font:smaller Verdana;vertical-align:top;text-align:left}
				body {margin:0px 0px 0px 15px}
				hr {margin:0px 0px 0px -15px;border:solid 1pt #dcdcdc}
				table {border-collapse:collapse;border:solid 1px #a9a9a9}
				th {background-color:#dcdcdc;border:solid 1px #a9a9a9;text-indent:2pt;font-weight:bolder}
				td {border:solid 1px #a9a9a9}
				pre{font-size:8pt}

				span.ok{background-color:lime;color:black;font-size:40%;color:lime;display:inline-block;}
				span.ko{background-color:red;color:white;font-size:40%;color:red;display:inline-block;}
				span.ignore{background-color:yellow;color:white;font-size:40%;color:yellow;display:inline-block;}

				p.testKo{
					cursor:hand;
					width:40px;
					height:40px;
					background-image:url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABYAAAAUCAIAAAAGHlpnAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAPBSURBVDhPY/hPMWDANOHXu1fvTx94v3bmy3kdT6bXP51c9XRi5ctpzS8O7vz57jWmehQj/v398+XKqXfLJ79Z0P5pUce3Re2/5jX9nFn9pb/odUv6w/Koh03Zb88dRzMF2Yh/3y4ce7uw89Pizt+r+v8s7fw1t/7ntIqfEwt/duX8akn5Vhf1MM/3WrrPkz2bkE1BGPH1xvk381q/LO36vbLv18LmX9PKf/bl/ehM/9mc+KM+5nt1+LfywM8FXg8SbM9E2rw8ewxuCtSIv5/ev10+6cOC1t/Lu/+c3g2R/jmz9kdVyM9JxVDuxrmfMhzeJ5pfCNI7kRP++/s3iDjUiK+n9r2eUftrcftvoOMn5P97dh8o9/fJne/VYX8f3QKxH978mGr7IcHiTbThoxDt7c5q97au+fv7N8KIN8snf5ha8WtO/Y9JRT9ak35OLYfY8O/NMxD59dOX8uAP8aZvow2eh2o98lfb7yh/tCTpz9fPCCOeTq7+Mrn0x6Tin51pP2ojvxX7/to0F+7b7ws63scav4vUfx2i+cRP5a6n/FF7qQ2hjj9fv0AYca8950d//o/uzB9Nsd/K/b/muX8tCYAb8SHd4W247psQjZf+yo885W65SJ2wEV3kbvTlznWEEbca0r61pX5vjv9WFfo13+NLluPvM/vhRvw8tuNNkPorP6VnXnL3nSWu2oscNheYY6/98co5hBF3GtLfV4Z/B+ov9P2a6fxz7XSI/u+b5kEYXxZ0vvSUfeQscdNO+Jwl/2YDnoVOel/ug0IaGiMP+2seZ7p/LwDa7/S1Kuzfl09AuR8b574N1vi+cwUoRD9/eBlnds9O+JIF/0lTngWanNsyI/58BimDGvH64PYLMTZvUm0/Jln+unISFIsvHr+NM33tr/Q6XPfPs4cgF505cNWC76Qx1y5djn413ht7d/77+xdhxO9PHy5Wpp4PNnoeafg8TPdZkOazANVnPgrPPGSfuog/chS9ayN4xZz7hBHHXj3W2Socy+P8/4ATBcIIIOvlhVOHwm1PeGnc9lW946V420P2tovkTQexG7aCVyx5z5lxHTPk2KPLMl+VdZql2sNLF+CBjZJTb2/fsMXLcIe93Dkn6YsOEudtRM5ZCZwx4z1mxHlQn22bFutMZdYpBtLnt26E60dxBUT0wZH922PcF5rJrDYU3mrIt8eQZ7sO1xoN9nmq7P1KnHOcDZ8f3YesH4sRoGB78+rKqkWbkoNmOehOsFTvtVSfYqm62Nvi9KS2T08foenHbgSmIvwiACPu1otCSuPAAAAAAElFTkSuQmCC);
					background-repeat:no-repeat
				}                
				p.testOk{
					cursor:hand;
					width:40px;
					height:40px;
					background-image:url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABQAAAAUCAIAAAAC64paAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAMTSURBVDhPpdTrL5tRGABw/80+LCKzGHHJqEt19KWoy2sW0a5D0a6hVcWoKu26Tlt1p1Uzd6WoqdVtQ0vEZW6xi2UXsw3r0FKq3ZHKeGUfljgfTvKcnF+ec3nOcbBdoTlcwdou40+/PopHH5M74sk98TQNiTbwIFF5l/AspnRE+O33xqVMCDz+YZSiJNS9kejML7XW7o49heyLRPqWx5t9lNabAEl9VXMdF/05nlx/laFOntjVLNp0mpPOpu2qinWBcInDm83h6JlsPYMxmOwvdG+davzrz/DW/k+mmqI3audt471HLfWbUtEKt3g2m62n502ms8aoJCVM6U8gq+K9i1zefV+z+zPct9QlneMt2CbUx62yjVLhIrtAz8h9TWONpmWOpOLlgUINV7XaTu0hRlRj0ptTEZimSho6UA1alYrNspJlDkjIGqMwhlPp2mRcbUC+imk5sZgth6x+WqwiNLgEhcDUgfv9lrbmnRrRKjdjKDFrNI0xlJKhJUfUBWYr0/cP944sR5U6UVwjLlaB8+a7IDBdS+4yNoITki7zRSM8WAFlDqfg5RhmB/Xw+ABMrdZJIhvuwIoQWB7iK3BDYEofESy4eD57Zkt3bDlmd2ehSz0y2ykms8lqtVZPlUUqMEBG1WHxNUE+fFcEJrZHi1YKOdPMp9PcXbPBZDYWdLNMR0YwqUYvDZej4QYoqi4ovCowtDzAi+uMwLm9dLaOXqBjUDUEsY5nXypotfqyMHlAjAKKkmGBDCnzDxb7YAS3EXhwWU3qjM0Ze5g+kJigjJTNVHw1fC7S5obL0HA9FuSMOJV+kAQF7lmsESCwwWSIKscS26IpfYQkZVxcM47YAoOjjpaDTWJwlWhI6hcsQaGfeLrmOa5triIwCNQLKh/erbiGMFIbHP8cD8ux0TIsKAlcBRoq9Q0WowIEns7Z10pe8C+Xpz3WLKqhEn9QwJAUFVaBxlcHgR57mtDLvcDJJfe6RCP898Owj+7sb7dPN5Fk99zyHd3ZTh6FN0DvxblJries/3h/UZ7X9qXR/wyv9JP8AUelhZbR6oNqAAAAAElFTkSuQmCC);
					background-repeat:no-repeat
				}
				p.testIgnore{
					cursor:hand;
					width:40px;
					height:40px;
					background-image:url(data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABQAAAATCAIAAAAf7rriAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAJeSURBVDhPY/hPAWDApfff328EjcWu+de3J49PWD47a3lym/Xlk0txmYJd89s7kz48UvnxLfDKEb4p1ZJ//vzGqh+L5r+/Pzw5ZfLvj+P//57//0tOreY+f2QWsZo/Pel9f8/h3//QRw9k//6TO7aTc26rGlGa//x8+vK83f+/BQsW+SnKa/d1G/z/zzetmvPCifWY+tGd/eH+1E/3ff//r01MtmFg8A3xsv7/X2j/GqbZLToENP/99fHJSZd/v5r+/8/sbjNkYkiY1a3+/z//15fcE8q47t44jqYfxeZPj2e/ueXz/3/v//+uuzbJKfA3Xjyi9P8Py9/v4ptnsM7rcsCp+d/fj8/Oevz6Vvz/f97//2pHDonqyMfdvSL4/wPDrw+ib66z9ZdyPn9yB1k/wubvr5a9ugb0IVCnw/+//FcvcCV46jy/yfb/GcPPR7x/Pois6GFeNTMSi+Z/fz68OOf+/bPX//9u/z9L/H/J+v8z07NznP/fMvy/w/DzCtOv95JPz3NOLOP9/u0DXD/U5q/PV7w6L/fvv+X/TzL/H7D/e8O8eBqLvy/7poXsQJt/nWP4eovn71vRpe2sy2fEomj+8Pb+9e0W3+9I//8s9f8W2/9bzN+fMViZMjIwxBWnJP5/wfDrLMP3c4x/ngg+Pcg0sd4eRfPrp2e3T+d/tIf7xWG2J7uZHu1gen6IcXk3U1WK2/k1Zo/3MN7Zwnh/K+OjXUwv9jGu6xdH0fz719cbh9Jn1LBPLmMDoklgcloVELFOqWCdVAZEIBEgmlrJuqBDE93PBLMuUWmbJFMAIcChwBwUNxQAAAAASUVORK5CYII=);
					background-repeat:no-repeat
			   }
				div.barContainer{width:100%;border:solid 1px black}
				div.trace{font:100% Courier}
				div.border{border:dotted 1px #dcdcdc;padding:2px 2px 2px 2px}
				div.contents{border:dotted 1px #dcdcdc;padding:2px 2px 2px 2px;background-color:#efefef}
									";
		}


		private void WriteEnvironmentInfo(TestRunResult run, StringBuilder result)
		{
			result.Append("<br /><a name=\"envInfo\" /><table width='900px'><tr><th colspan=\"2\">TestRun Environment Information</th></tr>");
			result.AppendFormat("<tr><th align=\"right\">MachineName</th><td>{0}</td></tr>", run.Computers.First());
			result.Append("<tr><th align=\"right\">TestAssemblies</th><td>");
			foreach (var a in run.Assemblies)
			{
				result.Append(a.FullName + " <br />");
			}
			result.Append("</td></tr>");
			result.AppendFormat("<tr><th align=\"right\">UserName</th><td>{0}</td></tr>", run.UserName);
			result.AppendFormat("<tr><th align=\"right\">TRX File</th><td>{0}</td></tr>", run.Name);
			result.Append("</table>");
		}

		private void WriteFooter(TestRunResult run, StringBuilder result)
		{
			result.Append("<hr style=\"border-style:dotted;color:#dcdcdc\"/>" +
							"<i style=\"width:100%;font:10pt Verdana;text-align:center;background-color:#dcdcdc\">" +
								"The VSTS Test Results HTML Viewer. (c) <a href=\"http://blogs.msdn.com/rido\">rido</a>'2011" + 
							"</i>");
			result.Append("</body></html>");
		}

		private void WriteBody(TestRunResult run, StringBuilder result)
		{
			WriteSummary(run, result);
			WriteSummaryDetails(run, result);
			WriteSlowerMethods(run, result);
			var classes = run.TestMethodRunList.GroupBy(m => m.TestClass);
			foreach (var c in classes)
			{
				TestClassRun tcr = run.TestClassList.First(tc => tc.FullName == c.Key);
				WriteClassResult(tcr, result);
			}
		}

		private void WriteSlowerMethods(TestRunResult run, StringBuilder result)
		{
			result.Append("<a name='slower' /><h5>TOP 5 Slower Methods</h5><table id=\"tSlowerMethods\" border='0' width='900px'>" +
								"<tr><th>TestMethod</th><th colspan='2'>Status</th><th>Duration</th></tr>");

			foreach (var m in run.TopSlowerMethods)
			{
                WriteTestMethodResult(m, result);
				//result.AppendFormat("<tr><td>{0}.{1}</td><td>{2}</td><td>{3}</td></tr>", 
                //        GetClassNameFromFullName(m.TestClass), m.TestMethodName, m.Status, m.Duration);
			}
			result.Append("</table>");
		}

		

		private void WriteSummary(TestRunResult run, StringBuilder result)
		{
			result.AppendFormat(@"<table id='tMainSummary'  border='0' width='900px'>
					  <tr>
						<th>Percent</th>
						<th>Status</th>
						<th>TotalTests</th>
						<th>Passed</th>
						<th>Failed</th>
						   <th>Inconclusive</th>
						<th>TimeTaken</th>
					  </tr>
					  <tr>
						<td>{0}%</td>
						<td width='350px' style='vertical-align:middle;font-size:200%'>{1}</td>
						<td>{2}</td>
						<td>{3}</td>
						<td>{4}</td>
						<td>{5}</td>
						<td>{6}</td>
					  </tr>
					</table>
					<br />",
					run.TotalPercent,
					CreateHtmlBars(run),
					run.TotalMethods,
					run.Passed,
					run.Failed,
					run.Inconclusive,
					run.TimeTaken);
		}

		private string CreateHtmlBars(I3ValueBar run)
		{
				double pcOk = run.PercentOK;
				double pcKo = run.PercentKO; ;     
				double pcIg = run.PercentIgnored; 	
				string result = "<div class=\"barContainer\">"; 	
				if (pcOk != 0)
				{
					result+=string.Format("<span class=\"ok\"  style=\"width:{0}%\" title=\"Passed!\" >p</span>", pcOk.ToString()); 	
				} 	
				if (pcKo != 0)
				{
					result += string.Format("<span class=\"ko\"  style=\"width:{0}%\" title=\"Failed\">f</span>", pcKo.ToString()); 	
				} 	
				if (pcIg != 0)
				{
					result += string.Format("<span class=\"ignore\"  style=\"width:{0}%\" title=\"Failed\">f</span>", pcIg.ToString()); 	
				} 	
				result+="</div>"; 	
				return result; 
 
		}

		private void WriteSummaryDetails(TestRunResult run, StringBuilder result)
		{
			result.Append(@"<table id='tSummaryDetail'  border='0' width='900px' >                              
							  <tr>
								<th colspan='7'>Failed TestClasses Summary (<a href='#' onclick='showAllTestClassesSummary()'> Show All</a> )</th>
							  </tr>
							  <tr>
								<th>Class Name</th>
								<th>Percent</th>
								<th>Status</th>
								<th>TestsPassed</th>
								<th>TestsFailed</th>
								<th>TestsIgnored</th>
								<th>Duration</th>
							  </tr>");
			foreach (var testClass in run.TestClassList)
			{
				result.AppendFormat("<tr style=\"display:{0}\" >", testClass.Status == "Succeed" ? "none" : "block");
				result.AppendFormat("<td><a href='#{0}'>{1}</a></td>", testClass.Name,testClass.Name);
				result.AppendFormat("<td>{0}%</td>", testClass.Percent); 
				result.AppendFormat("<td width=\"80px\">{0}</td>", CreateHtmlBars(testClass)); 
				result.AppendFormat("<td>{0}</td>", testClass.Success);
				result.AppendFormat("<td>{0}</td>", testClass.Failed);
				result.AppendFormat("<td>{0}</td>", testClass.Ignored);
				result.AppendFormat("<td>{0}</td>", testClass.Duration);
				result.AppendFormat("</tr>\r\n");                              
			}
			result.Append("</table>");
		}

		private void WriteClassResult(TestClassRun tcr, StringBuilder result)
		{
			   result.Append("<br />");
			   result.Append("<a name=\"detail\" />");
			   
			   
			result.AppendFormat(@"<h5></h5>
								  <a name='{0}' />
								  <table border='0' width='900px'>
									<tr>
									  <th colspan='4'>
										<b>{0}</b>
									  </th>
									</tr>",                               
							   tcr.Name);

			
			foreach (var method in tcr.TestMethods)
			{                                       
				WriteTestMethodResult(method, result);
			}
			result.Append("</table>\r\n");

		}

		private void WriteTestMethodResult(TestMethodRun m, StringBuilder result)
		{
			result.Append("<tr>\r\n");
            result.AppendFormat("<td>{0}.{1}</td>", GetClassNameFromFullName(m.TestClass), m.TestMethodName);
			switch (m.Status)
			{
				case  "Failed":
					AppendFailed(m, result);
					break;
				case "Passed":
					AppendPassed(m, result);
					break;
				default :
					AppendIgnored(m, result);
					break;
			}
			result.AppendFormat("<td>{0}</td>", m.Duration);
			result.Append("</tr>\r\n");
		}

		string GetClassNameFromFullName(string fullName)
		{
			int post = fullName.IndexOf(' ');
            return fullName.Substring(0, post-1);
		}

		void AppendMethodBullet(TestMethodRun m, StringBuilder result, string cssClass, string overColor, string outColor)
		{
			result.AppendFormat("<td><p class='{0}' title='Click to see the StackTrace' onmouseover='this.style.color=\"{1}\"' " +
											  "onmouseout='this.style.color=\"{2}\"' " +
											  "onclick=\"togle('{3}')\"></p></td>",
											  cssClass, overColor, outColor,m.GetHashCode());
			AppendErrorInfo(m, result);
		}

		private void AppendIgnored(TestMethodRun m, StringBuilder result)
		{
			AppendMethodBullet(m, result, "testIgnore", "white", "yellow");            
		}

		private void AppendPassed(TestMethodRun m, StringBuilder result)
		{
			AppendMethodBullet(m, result, "testOk", "green", "lime");            
		}

		private  void AppendFailed(TestMethodRun m, StringBuilder result)
		{
			AppendMethodBullet(m, result, "testKo", "orange", "red");
			
		}



		private  void AppendErrorInfo(TestMethodRun m, StringBuilder result)
		{
			result.AppendFormat(@"<td width='100%'>{0} <br /> {1}
									<div id='{2}' class='trace'  style='display:none'>
										  <div class='border'>{3}<br />{4}<br />{5}<br />{6}</div>
										<pre  class='failureInfo' >{6}</pre>
									</div>            
									</td>",
						m.Description ,
						m.ErrorInfo.Message,
						m.GetHashCode(),
						m.ErrorInfo.StdOut,
						m.ErrorInfo.StdErr,
						m.ErrorInfo.Message,
						m.ErrorInfo.StackTrace);
		}
	}
}  
