<?xml version="1.0" encoding="utf-8"?>

<xsl:stylesheet version="1.0"
    xmlns:xsl="http://www.w3.org/1999/XSL/Transform">

	<xsl:output method="html" indent="yes" encoding="utf-8"/>
	<xsl:strip-space elements="*"/>

	<xsl:template match="/">
		<table>
			<tr>
				<td>
					<h1>Условие задачи</h1>
					<xsl:apply-templates select="//introduction"/>
				</td>
			</tr>
			<tr>
				<td>
					<h1>Исходные данные</h1>
					<xsl:apply-templates select="//input-format"/>
				</td>
			</tr>
			<tr>
				<td>
					<h1>Результат</h1>
					<xsl:apply-templates select="//output-format"/>
				</td>
			</tr>
			<tr>
				<td>
					<xsl:apply-templates select="//samples"/>
				</td>
			</tr>
			<tr>
				<td>
					<xsl:apply-templates select="//hints"/>
				</td>
			</tr>
			<tr>
				<td>
					<xsl:apply-templates select="//author"/>
				</td>
			</tr>
		</table>
	</xsl:template>

	<xsl:template match="introduction">
		<xsl:apply-templates/>
	</xsl:template>

	<xsl:template match="input-format">
		<xsl:apply-templates/>
	</xsl:template>

	<xsl:template match="output-format">
		<xsl:apply-templates/>
	</xsl:template>

	<xsl:template match="samples">
		<xsl:if test="count(sample)">
			<h1>Примеры</h1>
			<table width="100%" cellspacing="0" cellpadding="5">
				<tr class="gridHeader">
					<th>Исходные данные</th>
					<th>Результат</th>
				</tr>
				<xsl:for-each select="sample">
					<tr>
						<xsl:choose>
							<xsl:when test="position() mod 2 = 0">
								<xsl:attribute name="class">gridDark</xsl:attribute>
							</xsl:when>
							<xsl:otherwise>
								<xsl:attribute name="class">gridLight</xsl:attribute>
							</xsl:otherwise>
						</xsl:choose>
						<td>
							<pre>
								<xsl:value-of select="input"/>
							</pre>
						</td>
						<td>
							<pre>
								<xsl:value-of select="output"/>
							</pre>
						</td>
					</tr>
				</xsl:for-each>
			</table>
		</xsl:if>
	</xsl:template>

	<xsl:template match="hints">
		<xsl:if test="count(hint)">
			<h2>Подсказки</h2>
			<ol>
				<xsl:for-each select="hint">
					<li>
						<xsl:apply-templates/>
					</li>
				</xsl:for-each>
			</ol>
		</xsl:if>
	</xsl:template>

	<xsl:template match="author">
		<hr/>
		<strong>Автор: </strong>
		<xsl:value-of select="."/>
	</xsl:template>

	<xsl:template match="b">
		<b>
			<xsl:value-of select="."/>
		</b>
	</xsl:template>

	<xsl:template match="i">
		<i>
			<xsl:value-of select="."/>
		</i>
	</xsl:template>

	<xsl:template match="u">
		<u>
			<xsl:value-of select="."/>
		</u>
	</xsl:template>

	<xsl:template match="ulist">
		<xsl:if test="count(item)">
			<ul>
				<xsl:for-each select="item">
					<li>
						<xsl:apply-templates/>
					</li>
				</xsl:for-each>
			</ul>
		</xsl:if>
	</xsl:template>

	<xsl:template match="olist">
		<xsl:if test="count(item)">
			<ol>
				<xsl:for-each select="item">
					<li>
						<xsl:apply-templates/>
					</li>
				</xsl:for-each>
			</ol>
		</xsl:if>
	</xsl:template>

	<xsl:template match="table">
		<xsl:for-each select="row">
			<xsl:for-each select="col">
				<xsl:apply-templates/>
			</xsl:for-each>
		</xsl:for-each>
	</xsl:template>

	<xsl:template match="picture">
		<img>
			<xsl:attribute name="src">pictures/<xsl:value-of select="@file"/></xsl:attribute>
		</img>
	</xsl:template>

	<xsl:template match="par">
		<p>
			<xsl:apply-templates/>
		</p>
	</xsl:template>

	<xsl:template match="sup">
		<sup>
			<xsl:value-of select="."/>
		</sup>
	</xsl:template>

	<xsl:template match="sub">
		<sub>
			<xsl:value-of select="."/>
		</sub>
	</xsl:template>
	
	<xsl:template match="le">
		<xsl:text>&amp;le;</xsl:text>
	</xsl:template>

	<xsl:template match="ge">
		<xsl:text>&amp;ge;</xsl:text>
	</xsl:template>
</xsl:stylesheet>

