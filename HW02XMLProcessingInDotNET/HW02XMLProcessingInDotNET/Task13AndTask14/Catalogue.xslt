<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
  <xsl:output method='html' version='1.0' encoding='UTF-8' indent='yes' />
  <xsl:template match="/">
    <xsl:text disable-output-escaping="yes">     
    </xsl:text>
    <html>
      <body bgcolor="#E6E6FA">
        <h2>Catalogue</h2>
        <table border="1">
          <tr bgcolor="#4587F5">
            <th align="left">Title</th>
            <th align="left">Artist</th>
            <th align="left">Year</th>
            <th align="left">Producer</th>
          </tr>
          <xsl:for-each select="Catalogue/Albums/Album">
            <tr bgcolor="#8AC007">
              <td>
                <xsl:value-of select="Name" />
              </td>
              <td>
                <xsl:value-of select="Artist" />
              </td>
              <td>
                <xsl:value-of select="Year" />
              </td>
              <td>
                <xsl:value-of select="Producer" />
              </td>
            </tr>
          </xsl:for-each>
        </table>
      </body>
    </html>
  </xsl:template>
</xsl:stylesheet>
