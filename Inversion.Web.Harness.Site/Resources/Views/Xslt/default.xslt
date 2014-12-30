<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0"
				xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
				xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
	<xsl:output method="xml" indent="yes"/>

	<xsl:template match="*">
		<xsl:apply-templates />
	</xsl:template>
	
	<xsl:template match="/">
		<html>
			<head>
				<title>web harness</title>
			</head>
			<body>
				<xsl:apply-templates />
			</body>
		</html>
	</xsl:template>

	<xsl:template match="records/item[@name='messages']/list">
		<ul>
			<xsl:apply-templates />
		</ul>
	</xsl:template>

	<xsl:template match="list/item">
		<li>
			<xsl:value-of select="." />
		</li>
	</xsl:template>

	<xsl:template match="records/item[@name='event-trace']/list">
		<ul>
			<xsl:apply-templates />
		</ul>
	</xsl:template>

	<xsl:template match="list/event">
		<li>
			<xsl:value-of select="@message" />
			<ul>
				<xsl:apply-templates />
			</ul>
		</li>
	</xsl:template>

	<xsl:template match="list/event/params/item">
		<li>
			<xsl:value-of select="@name" /> = <xsl:value-of select="@value" />
		</li>
	</xsl:template>
	
</xsl:stylesheet>
