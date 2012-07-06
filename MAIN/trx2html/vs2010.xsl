<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
    xmlns:t="http://microsoft.com/schemas/VisualStudio/TeamTest/2010"
    xmlns:trxreport="urn:my-scripts"
>
    <xsl:output method="html" indent="yes"/>
    <xsl:key name="TestMethods" match="t:TestMethod" use="@className"/>
    <!--<xsl:namespace-alias stylesheet-prefix="t" result-prefix="#default"/>-->

  <msxsl:script language="C#" implements-prefix="trxreport">
    public string RemoveAssemblyName(string asm) {
    return asm.Substring(0,asm.IndexOf(','));
    }
    public string RemoveNamespace(string asm) {
    int coma = asm.IndexOf(',');
    return asm.Substring(coma + 2, asm.Length - coma - 2);
    }
  </msxsl:script>
  
  <xsl:template match="/" >

    <html>
      <head>
        <link type="text/css" rel="StyleSheet"  href="trx2html.css"   />
        <script language="javascript" type="text/javascript" src="trx2html.js"></script>
        <title>
          TRX Report -
          <xsl:value-of select="/t:TestRun/t:TestRunConfiguration/t:Description"/>
        </title>
        
      </head>
      <body onload="summary();">
        <a name="__top" />

        <h3>
        <xsl:value-of select="/t:TestRun/t:TestRunConfiguration/t:Description"/>
        </h3>
        <div class="contents">
          <a href="#totals">Totals</a>
          |
          <a href="#summary">Summary</a>
          |
          <a href="#detail">Detail</a>
          |
          <a href="#envInfo">Environment Information</a>
        </div>
        <br />
        <a name="totals" />
        <table id="tMainSummary"  border="0">
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
            <td></td>
            <td width="350px" style="vertical-align:middle;font-size:200%"></td>
            <td>
              <xsl:value-of select="/t:TestRun/t:ResultSummary/t:Counters/@total"/>
            </td>
            <td>
              <!--<xsl:value-of select="/Tests/TestRun/result/passedTestCount"/>-->
              <xsl:value-of select="/t:TestRun/t:ResultSummary/t:Counters/@passed"/>
            </td>

            <td>
              <xsl:value-of select="/t:TestRun/t:ResultSummary/t:Counters/@failed"/>
              
            </td>

            <td>
              <xsl:value-of select="/t:TestRun/t:ResultSummary/t:Counters/@inconclusive"/>
              
            </td>
            <td></td>
          </tr>
        </table>
        
        
        <br />

        <a name="summary" />
        <table id="tSummaryDetail"  border="0">
          <tr>
            <th>TestClasses Summary</th>
            <th>Percent</th>
            <th>Status</th>
            <th>TestsPassed</th>
            <th>TestsFailed</th>
            <th>TestsIgnored</th>
            <th>Duration</th>
          </tr>
          <xsl:for-each select="//t:TestMethod[generate-id(.)=generate-id(key('TestMethods', @className))]">
            <tr>
              <td>
                <a>
                  <xsl:attribute name="href">
                    <xsl:value-of select="'#'"/>
                    <xsl:value-of select="generate-id(@className)"/>
                  </xsl:attribute>
                  <xsl:value-of select="trxreport:RemoveAssemblyName(@className)" />
                </a>
              </td>

              <!-- Percent -->
              <td></td>

              <!-- status -->
              <td width="80px"></td>

              <!-- success -->
              <td></td>

              <!-- failed-->
              <td></td>

              <!-- ignored-->
              <td></td>


              <!-- Duration -->
              <td></td>
            </tr>
          </xsl:for-each>
        </table>

        <br />
        <a name="detail" />
        <i>Test Class Detail</i>

        <xsl:for-each select="//t:TestMethod[generate-id(.)=generate-id(key('TestMethods', @className))]">
          <h5>

          </h5>

          <a name="{generate-id(@className)}" />
          <table border="0">
            <tr>
              <th colspan="4">
                <b>
                  <xsl:value-of select="trxreport:RemoveAssemblyName(@className)" />
                </b>
              </th>
            </tr>


            <xsl:for-each select="key('TestMethods', @className)" >
              <tr>
                <xsl:call-template name="tDetails">
                  <xsl:with-param name="testId" select="./../@id" />
                  <xsl:with-param name="testDescription" select="./../t:Description" />
                </xsl:call-template>
              </tr>
            </xsl:for-each>
          </table>
          <a href="#__top">Back to top</a>
          <br />
        </xsl:for-each>

        <br />
        <a name="envInfo" />
        <xsl:call-template name="envInfo" />
        <hr style="border-style:dotted;color:#dcdcdc"/>
        <i style="width:100%;font:10pt Verdana;text-align:center;background-color:#dcdcdc">
          The VSTS Test Results HTML Viewer. (c) <a href="http://blogs.msdn.com/rido">rido</a>'2010
        </i>

      </body>
    </html>
  </xsl:template>
  <xsl:template name="tDetails">
    <xsl:param name="testId" />
    <xsl:param name="testDescription" />
    <xsl:for-each select="/t:TestRun/t:Results/t:UnitTestResult[@testId=$testId]">
      <td>
        <xsl:value-of select="@testName"/>
      </td>

      <xsl:choose>
        <xsl:when test="@outcome='Failed'">
          <td>
            <p class="testKo"
               title="Click to see the StackTrace"
               onmouseover="this.style.color='orange'"
              onmouseout="this.style.color='red'"
              onclick="togle('{generate-id(.)}')">n</p>
          </td>
          <td width="300px">

            <xsl:value-of select="$testDescription"/>
            <br />
            
            <xsl:value-of select="t:Output/t:ErrorInfo/t:Message/text()"/>
            
            <div id="{generate-id(.)}" class="trace"  style="display:none">
              <xsl:call-template name="debugInfo">
                <xsl:with-param name="testId" select="$testId" />
              </xsl:call-template>
              <pre  class="failureInfo" >
                <xsl:value-of select="t:Output/t:ErrorInfo/t:StackTrace/text()"/>
              </pre>
            </div>
            
          </td>
        </xsl:when>
        <xsl:when test="@outcome='Passed'">
          <td>
            <p class="testOk"
              title="Click to see Test Trace"
               onmouseover="this.style.color='green'"
              onmouseout="this.style.color='lime'"
              onclick="togle('{generate-id(.)}')">n</p>
          </td>
          <td width="300px">

            <xsl:value-of select="$testDescription"/>
            
            <div id="{generate-id(.)}" class="trace" style="display:none">
              <xsl:call-template name="debugInfo">
                <xsl:with-param name="testId" select="$testId" />
              </xsl:call-template>
            </div>
            
          </td>
        </xsl:when>
        <xsl:otherwise>
          <td>
            <p class="testIgnore"
              title="Click to see test Trace"
               onmouseover="this.style.color='white'"
              onmouseout="this.style.color='yellow'"
              onclick="togle('{generate-id(.)}')">n</p>
          </td>
          <td width="300px">
            <xsl:value-of select="$testDescription"/>
            <br />

            <xsl:value-of select="t:Output/t:ErrorInfo/t:Message/text()"/>
                        
            <div id="{generate-id(.)}" class="trace" style="display:none">
              <xsl:call-template name="debugInfo">
                <xsl:with-param name="testId" select="$testId" />
              </xsl:call-template>

            </div>
            
          </td>
        </xsl:otherwise>
      </xsl:choose>
      <td>
        <xsl:value-of select="@duration" />
      </td>
    </xsl:for-each>
  </xsl:template>

  <xsl:template name="debugInfo">
    <xsl:param name="testId" />
    <xsl:for-each select="/t:TestRun/t:Results/t:UnitTestResult[@testId=$testId]/t:Output">

      <div class="border">
        <xsl:value-of select="t:StdOut"/>
        <br />
        <xsl:value-of select="t:StdErr"/>
        <br />
        <xsl:value-of select="t:ErrorInfo/t:Message"/>
        <br />
        <xsl:value-of select="t:ErrorInfo/t:StackTrace"/>
      </div>
    </xsl:for-each>
  </xsl:template>

  <xsl:template name="envInfo">
    <table>
      <tr>
        <th colspan="2">TestRun Environment Information</th>
      </tr>
      <tr>
        <th align="right">TestCodebase</th>
        <td>
          <xsl:value-of select="/t:TestRun/t:TestDefinitions/t:UnitTest/t:TestMethod/@codeBase"/>
        </td>
      </tr>
      <tr>
        <th align="right">AssemblyUnderTest</th>
        <td>
          <xsl:value-of select="//t:DeploymentItem/@filename" />
        </td>
      </tr>
      <tr>
        <th align="right">MachineName</th>
        <td>
          <xsl:value-of select="//t:UnitTestResult/@computerName"/>
        </td>
      </tr>
      <tr>
        <th align="right">UserName</th>
        <td>
          <xsl:value-of select="/t:TestRun/@runUser"/>
        </td>
      </tr>
      <tr>
        <th align="right">Original TRXFile</th>
        <td>
          <xsl:value-of select="/t:TestRun/@name"/>
        </td>
      </tr>
    </table>

  </xsl:template>



  <!--<xsl:template match="@* | node()">
        <xsl:copy>
            <xsl:apply-templates select="@* | node()"/>
        </xsl:copy>
    </xsl:template>-->
</xsl:stylesheet>
