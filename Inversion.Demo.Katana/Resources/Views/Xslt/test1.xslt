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
		<result>
			<head>
				<title>Test 1</title>
			</head>
			<body>
				<xsl:apply-templates />
			</body>
		</result>
	</xsl:template>

	<xsl:template match="records/item[@name='eventTrace']/list">
		<events>
			<xsl:apply-templates />
		</events>
	</xsl:template>

	<xsl:template match="list/event">
		<event>
			<xsl:attribute name="message">
				<xsl:value-of select="@message" />
			</xsl:attribute>
			<params>
				<xsl:apply-templates />
			</params>
		</event>
	</xsl:template>

	<xsl:template match="list/event/params/item">
		<xsl:attribute name="{@name}">
			<xsl:value-of select="@value" />
		</xsl:attribute>

	</xsl:template>

</xsl:stylesheet>
