<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl"
>
	<xsl:output method="xml" indent="yes"/>

	<xsl:template match="/">
		<events>
			<xsl:apply-templates select="//events/event" />
		</events>
	</xsl:template>

	<xsl:template match="event">
		<event message="{@message}">
			<xsl:apply-templates select="params/@*" />
		</event>
	</xsl:template>

	<xsl:template match="params/@*">
		<xsl:attribute name="{name()}">
			<xsl:value-of select="."/>
		</xsl:attribute>
	</xsl:template>

</xsl:stylesheet>
