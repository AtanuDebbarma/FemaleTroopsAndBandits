<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
	<xsl:output omit-xml-declaration="yes"/>

	<!-- Append FTR T6 Sword Sister upgrades next to vanilla trees; vanilla T5 mercs stay intact for tavern hiring. -->
	<xsl:variable name="gallantT6">NPCCharacter.ftr_sword_sisters_gallant_t6</xsl:variable>
	<xsl:variable name="veteranT6">NPCCharacter.ftr_sword_sisters_veteran_t6</xsl:variable>

	<xsl:template match="@*|node()">
		<xsl:copy>
			<xsl:apply-templates select="@*|node()"/>
		</xsl:copy>
	</xsl:template>

	<xsl:template match="NPCCharacter[@id='sword_sisters_sister_t5']/upgrade_targets[not(upgrade_target[@id='NPCCharacter.ftr_sword_sisters_gallant_t6'])]">
		<xsl:call-template name="append-target">
			<xsl:with-param name="target" select="$gallantT6"/>
		</xsl:call-template>
	</xsl:template>

	<xsl:template match="NPCCharacter[@id='sword_sisters_sister_t5'][not(upgrade_targets)]">
		<xsl:call-template name="add-targets">
			<xsl:with-param name="target" select="$gallantT6"/>
		</xsl:call-template>
	</xsl:template>

	<xsl:template match="NPCCharacter[@id='sword_sisters_sister_infantry_t5']/upgrade_targets[not(upgrade_target[@id='NPCCharacter.ftr_sword_sisters_veteran_t6'])]">
		<xsl:call-template name="append-target">
			<xsl:with-param name="target" select="$veteranT6"/>
		</xsl:call-template>
	</xsl:template>

	<xsl:template match="NPCCharacter[@id='sword_sisters_sister_infantry_t5'][not(upgrade_targets)]">
		<xsl:call-template name="add-targets">
			<xsl:with-param name="target" select="$veteranT6"/>
		</xsl:call-template>
	</xsl:template>

	<xsl:template name="append-target">
		<xsl:param name="target"/>
		<xsl:copy>
			<xsl:apply-templates select="@*|node()"/>
			<upgrade_target id="{$target}"/>
		</xsl:copy>
	</xsl:template>

	<xsl:template name="add-targets">
		<xsl:param name="target"/>
		<xsl:copy>
			<xsl:apply-templates select="@*|node()"/>
			<upgrade_targets>
				<upgrade_target id="{$target}"/>
			</upgrade_targets>
		</xsl:copy>
	</xsl:template>
</xsl:stylesheet>
