﻿using System.Collections.Generic;

namespace ML.Utils.IntegrityDirect
{
	public sealed class IntegrityDirectCodes
	{

		#region MatchCodes

		public static string GetMatchDescription(string code)
		{
			return !string.IsNullOrWhiteSpace(code) && MatchCodes.ContainsKey(code) ? MatchCodes[code] : string.Empty;
		}

		public static Dictionary<string, string> MatchCodes = new Dictionary<string, string>
		{
            {"2", "First/Last/Home Address/Home city/Home State/Home Zip/Phone/DOB(mm/dd/yyyy)/ID" },
			{"3", "First/Last/Home Address/Home city/Home State/Home Zip/DOB(mm/dd/yyyy)/ID" },
			{"4", "First/Last/Home Address/Home city/Home State/Home Zip/DOB(mm/dd/yyyy)/SSN4" },
			{"5", "First/Last/Suffix/Mail Address/Mail Zip/DOB(mm/dd/yyyy)" },
			{"6", "First/Last/Home Address/Home city/Home State/Home Zip/Phone4/DOB(mm/dd/yyyy)/ID" },
			{"10", "First/Last/Suffix/Mail Address/Mail Zip/DOB(mm/yyyy)" },
			{"15", "First/Last/Suffix/Mail Address/Mail Zip/DOB(yyyy)" },
			{"20", "First/Last/Suffix/Mail Address/Mail Zip/DOB(yyy)" },
			{"24", "First/Last/Mail Address/Mail Zip/DOB(mm/dd/yyyy)/ID" },
			{"25", "First/Last/Mail Address/Mail Zip/DOB(mm/dd/yyyy)" },
			{"26", "First Initial/Last/Mail Address/Mail Zip/DOB(mm/dd/yyyy)" },
			{"29", "First/Last/Mail Address/Mail Zip/DOB(mm/yyyy)/ID" },
			{"30", "First/Last/Mail Address/Mail Zip/DOB(mm/yyyy)" },
			{"31", "First Initial/Last/Mail Address/Mail Zip/DOB(mm/yyyy)" },
			{"34", "First/Last/Mail Address/Mail Zip/DOB(yyyy)/ID" },
			{"35", "First/Last/Mail Address/Mail Zip/DOB(yyyy)" },
			{"36", "First Initial/Last/Mail Address/Mail Zip/DOB(yyyy)" },
			{"39", "First/Last/Mail Address/Mail Zip/DOB(yyy)/ID" },
			{"40", "First/Last/Mail Address/Mail Zip/DOB(yyy)" },
			{"41", "First Initial/Last/Mail Address/Mail Zip/DOB(yyy)" },
			{"45", "First/Last/Suffix/Home Address/Home Zip/DOB(mm/dd/yyyy)" },
			{"50", "First/Last/Suffix/Home Address/Home Zip/DOB(mm/yyyy)" },
			{"55", "First/Last/Suffix/Home Address/Home Zip/DOB(yyyy)" },
			{"60", "First/Last/Suffix/Home Address/Home Zip/DOB(yyy)" },
			{"64", "First/Last/Home Address/Home Zip/DOB(mm/dd/yyyy)/ID" },
			{"65", "First/Last/Home Address/Home Zip/DOB(mm/dd/yyyy)" },
			{"66", "First Initial/Last/Home Address/Home Zip/DOB(mm/dd/yyyy)" },
			{"69", "First/Last/Home Address/Home Zip/DOB(mm/yyyy)/ID" },
			{"70", "First/Last/Home Address/Home Zip/DOB(mm/yyyy)" },
			{"71", "First Initial/Last/Home Address/Home Zip/DOB(mm/yyyy)" },
			{"74", "First/Last/Home Address/Home Zip/DOB(yyyy)/ID" },
			{"75", "First/Last/Home Address/Home Zip/DOB(yyyy)" },
			{"76", "First Initial/Last/Home Address/Home Zip/DOB(yyyy)" },
			{"79", "First/Last/Home Address/Home Zip/DOB(yyy)/ID" },
			{"80", "First/Last/Home Address/Home Zip/DOB(yyy)" },
			{"81", "First Initial/Middle Initial/Last/Mail Address/Mail Zip/DOB(yyy)" },
			{"85", "First/Last/Suffix/Mail Address/Mail Zip" },
            {"86", "First/Last/Home Address/Home city/Home State/Home Zip" },
			{"90", "First/Last/Suffix/Home Address/Home Zip" },
			{"95", "First/Last/Mail Address/Mail Zip" },
			{"96", "First/Middle/Last/Home Address/Home city/Home Zip" },
			{"97", "First Initial/Middle/Last/Home Address/Home city/Home Zip" },
			{"98", "First/Last/Home Address/Home city/Home Zip" },
			{"99", "First Initial/Last/Home Address/Home city/Home Zip" },
			{"100", "First/Last/Home Address/Home Zip" },
			{"102", "First Initial/Last/Mail Address/Mail Zip" },
			{"103", "First Initial/Last/Home Address/Home Zip" },
			{"105", "First/Last/Suffix/Mail Zip/Mail Zip4/DOB(mm/dd/yyyy)" },
			{"110", "First/Last/Suffix/Mail Zip/Mail Zip4/DOB(mm/yyyy)" },
			{"115", "First/Last/Suffix/Mail Zip/Mail Zip4/DOB(yyyy)" },
			{"120", "First/Last/Suffix/Mail Zip/Mail Zip4/DOB(yyy)" },
			{"125", "First/Last/Mail Zip/Mail Zip4/DOB(mm/dd/yyyy)" },
			{"130", "First/Last/Mail Zip/Mail Zip4/DOB(mm/yyyy)" },
			{"135", "First/Last/Mail Zip/Mail Zip4/DOB(yyyy)" },
			{"140", "First/Last/Mail Zip/Mail Zip4/DOB(yyy)" },
			{"144", "First/Last/Mail Zip/DOB(mm/dd/yyyy)/ID" },
			{"145", "First/Last/Mail Zip/DOB(mm/dd/yyyy)" },
			{"149", "First/Last/Home Zip/DOB(mm/dd/yyyy)/ID" },
			{"150", "First/Last/Home Zip/DOB(mm/dd/yyyy)" },
			{"154", "First/Last/Mail Zip/DOB(mm/yyyy)/ID" },
			{"155", "First/Last/Mail Zip/DOB(mm/yyyy)" },
			{"156", "First/Last/Gender/Home Zip/DOB(mm/yyyy)" },
			{"159", "First/Last/Home Zip/DOB(mm/yyyy)/ID" },
			{"160", "First/Last/Home Zip/DOB(mm/yyyy)" },
			{"164", "First/Last/Mail Zip/DOB(yyyy)/ID" },
			{"165", "First/Last/Mail Zip/DOB(yyyy)" },
			{"169", "First/Last/Home Zip/DOB(yyyy)/ID" },
			{"170", "First/Last/Home Zip/DOB(yyyy)" },
			{"174", "First/Last/Mail Zip/DOB(yyy)/ID" },
			{"175", "First/Last/Mail Zip/DOB(yyy)" },
			{"180", "First/Last/Home Zip/DOB(yyy)" },
			{"185", "First/Last/Suffix/Mail Zip/Mail Zip4" },
			{"190", "First/Last/Mail Zip/Mail Zip4" },
			{"195", "First Initial/Last/Gender/Mail Zip/DOB(mm/dd/yyyy)" },
			{"200", "First Initial/Last/Gender/Home Zip/DOB(mm/dd/yyyy)" },
			{"205", "First Initial/Last/Gender/Mail Zip/DOB(mm/yyyy)" },
			{"210", "First Initial/Last/Gender/Home Zip/DOB(mm/yyyy)" },
			{"215", "First Initial/Last/Gender/Mail Zip/DOB(yyyy)" },
			{"220", "First Initial/Last/Gender/Home Zip/DOB(yyyy)" },
			{"225", "First Initial/Last/Gender/Mail Zip/DOB(yyy)" },
			{"230", "First Initial/Last/Gender/Home Zip/DOB(yyy)" },
			{"235", "First Initial/Last/Mail Zip/DOB(mm/dd/yyyy)" },
			{"240", "First Initial/Last/Home Zip/DOB(mm/dd/yyyy)" },
			{"245", "First Initial/Last/Mail Zip/DOB(mm/yyyy)" },
			{"250", "First Initial/Last/Home Zip/DOB(mm/yyyy)" },
			{"255", "First Initial/Last/Mail Zip/DOB(yyyy)" },
			{"260", "First Initial/Last/Home Zip/DOB(yyyy)" },
			{"265", "First Initial/Last/Mail Zip/DOB(yyy)" },
			{"270", "First Initial/Last/Home Zip/DOB(yyy)" },
			{"275", "First/Last/Gender/Mail Zip" },
			{"280", "First/Last/Gender/Home Zip" },
			{"285", "Last/Gender/Mail Zip/DOB(mm/dd/yyyy)" },
			{"290", "Last/Gender/Home Zip/DOB(mm/dd/yyyy)" },
			{"295", "Last/Gender/Mail Zip/DOB(mm/yyyy)" },
			{"300", "Last/Gender/Home Zip/DOB(mm/yyyy)" },
			{"305", "Last/Gender/Mail Zip/DOB(yyyy)" },
			{"310", "Last/Gender/Home Zip/DOB(yyyy)" },
			{"315", "Last/Gender/Mail Zip/DOB(yyy)" },
			{"319", "Last/Home Address/Home city/Home Zip" },
			{"320", "Last/Gender/Home Zip/DOB(yyy)" },
			{"321", "Last/Mail Address/Mail Zip" },
			{"322", "Last/Home Address/Home Zip" },
			{"323", "First/Last/Home Address/Home city" },
			{"324", "First Initial/Last/Home Address/Home city" },
			{"325", "First/Last/Mail Zip" },
			{"326", "Last/Home Address/Home city" },
			{"329", "First/Last/Home city/Home Zip" },
			{"330", "First/Last/Home Zip" },
			{"332", "First/Home Address/Home Zip/DOB(mm/dd/yyyy)" },
			{"333", "First/Mail Address/Mail Zip/DOB(mm/dd/yyyy)" },
			{"334", "Last/Mail Address/Mail Zip/DOB(mm/dd/yyyy)" },
			{"335", "Last/Mail Zip/DOB(mm/dd/yyyy)" },
			{"340", "Last/Home Zip/DOB(mm/dd/yyyy)" },
			{"345", "Last/Mail Zip/DOB(mm/yyyy)" },
			{"350", "Last/Home Zip/DOB(mm/yyyy)" },
			{"355", "Last/Mail Zip/DOB(yyyy)" },
			{"360", "Last/Home Zip/DOB(yyyy)" },
			{"364", "Last/Home city/DOB(mm/dd/yyyy)" },
			{"365", "Last/Mail Zip/DOB(yyy)" },
			{"366", "Last/Home city/DOB(mm/yyyy)" },
			{"367", "Last/Home city/DOB(yyyy)" },
			{"368", "First/Last/Home Address/Home city/Home State" },
			{"369", "First Initial/Last/Home Address/Home city/Home State" },
			{"370", "Last/Home Zip/DOB(yyy)" },
			{"371", "Last/Home Address/Home State/DOB(mm/dd/yyyy)" },
			{"372", "First Initial/Last/Gender/Home Address/Home State/DOB(mm/yyyy)" },
			{"373", "First Initial/Last/Gender/Home State/Phone/DOB(mm/dd/yyyy)" },
			{"374", "First/Last/Gender/Home State/Phone/DOB(mm/yyyy)" },
			{"375", "First/Last/Mail State/DOB(mm/dd/yyyy)" },
			{"376", "First/Last/Home Address/Home State" },
			{"377", "First Initial/Last/Home Address/Home State" },
			{"378", "First/Last/Mail Address/Mail State" },
			{"379", "First Initial/Last/Mail Address/Mail State" },
			{"380", "First/Last/Home State/DOB(mm/dd/yyyy)" },
			{"381", "First/Last/Home State/Phone/DOB(mm/dd/yyyy)" },
			{"382", "First/Last/Home State/Phone4/DOB(mm/dd/yyyy)" },
			{"383", "First/Last/Home State/Phone/DOB(mm/yyyy)" },
			{"384", "First/Last/Home State/Phone4/DOB(mm/yyyy)" },
			{"385", "First/Last/Mail State/DOB(mm/yyyy)" },
			{"386", "First/Last/Home State/Phone/DOB(yyyy)" },
			{"387", "First/Last/Home State/Phone4/DOB(yyyy)" },
			{"388", "First Initial/Last/Home State/Phone/DOB(mm/yyyy)" },
			{"389", "First Initial/Last/Home State/Phone4/DOB(mm/yyyy)" },
			{"390", "First/Last/Home State/DOB(mm/yyyy)" },
			{"391", "First Initial/Last/Home State/Phone/DOB(yyyy)" },
			{"392", "First Initial/Last/Home State/Phone4/DOB(yyyy)" },
			{"393", "First Initial/Last/Home Zip/Phone" },
			{"394", "First Initial/Last/Home Zip/Phone4" },
			{"395", "First/Last/Mail State/DOB(yyyy)" },
			{"396", "Last/Home Zip/Phone" },
			{"397", "Last/Home Zip/Phone4" },
			{"398", "First Initial/Last/Phone" },
			{"399", "First Initial/Last/Phone4" },
			{"400", "First/Last/Home State/DOB(yyyy)" },
			{"405", "First/Last/Mail State/DOB(yyy)" },
			{"410", "First/Last/Home State/DOB(yyy)" },
			{"411", "First/Last/Gender/Home State/DOB(mm/dd/yyyy)" },
			{"412", "Last/Gender/Home Address/Home State/DOB(mm/dd/yyyy)" },
			{"413", "First/Last/Gender/Home State/DOB(mm/yyyy)" },
			{"415", "First Initial/Last/Gender/Mail State/DOB(mm/dd/yyyy)" },
			{"420", "First Initial/Last/Gender/Home State/DOB(mm/dd/yyyy)" },
			{"425", "First Initial/Last/Gender/Mail State/DOB(mm/yyyy)" },
			{"430", "First Initial/Last/Gender/Home State/DOB(mm/yyyy)" },
			{"435", "First Initial/Last/Gender/Mail State/DOB(yyyy)" },
			{"440", "First Initial/Last/Gender/Home State/DOB(yyyy)" },
			{"445", "First Initial/Last/Gender/Mail State/DOB(yyy)" },
			{"450", "First Initial/Last/Gender/Home State/DOB(yyy)" },
			{"455", "First Initial/Last/Mail State/DOB(mm/dd/yyyy)" },
			{"460", "First Initial/Last/Home State/DOB(mm/dd/yyyy)" },
			{"461", "First Initial/Last/Home State/Phone/DOB(mm/dd/yyyy)" },
			{"462", "First Initial/Last/Home State/Phone4/DOB(mm/dd/yyyy)" },
			{"465", "First Initial/Last/Mail State/DOB(mm/yyyy)" },
			{"470", "First Initial/Last/Home State/DOB(mm/yyyy)" },
			{"475", "First Initial/Last/Mail State/DOB(yyyy)" },
			{"480", "First Initial/Last/Home State/DOB(yyyy)" },
			{"485", "First Initial/Last/Mail State/DOB(yyy)" },
			{"490", "First Initial/Last/Home State/DOB(yyy)" },
			{"491", "First Initial/Last/Gender/Home Address/Home Zip/DOB(mm/yyyy)" },
			{"492", "First Initial/Last/Gender/Mail Zip/Mail Zip4/DOB(mm/yyyy)" },
			{"495", "First Initial/Last/Gender/Mail Zip" },
			{"500", "First Initial/Last/Gender/Home Zip" },
			{"505", "First Initial/Last/Mail Zip" },
			{"509", "First Initial/Last/Home city/Home Zip" },
			{"510", "First Initial/Last/Home Zip" },
			{"515", "Last/Gender/Mail Zip" },
			{"520", "Last/Gender/Home Zip" },
			{"525", "Gender/Mail Zip/DOB(mm/dd/yyyy)" },
			{"530", "Gender/Home Zip/DOB(mm/dd/yyyy)" },
			{"535", "Gender/Mail Zip/DOB(mm/yyyy)" },
			{"540", "Gender/Home Zip/DOB(mm/yyyy)" },
			{"545", "Gender/Mail Zip/DOB(yyyy)" },
			{"550", "Gender/Home Zip/DOB(yyyy)" },
			{"555", "Gender/Mail Zip/DOB(yyy)" },
			{"560", "Gender/Home Zip/DOB(yyy)" },
			{"565", "Last/Mail Zip" },
			{"567", "Last/Home city/Home Zip" },
			{"568", "First/Last/Home city/Home State" },
			{"569", "First Initial/Last/Home city/Home State" },
			{"570", "Last/Home Zip" },
			{"571", "First/Last/Mail State" },
			{"572", "First/Last/Home State" },
			{"573", "First Initial/Last/Mail State" },
			{"574", "First Initial/Last/Home State" },
			{"575", "Mail Zip/DOB(mm/dd/yyyy)" },
			{"580", "Home Zip/DOB(mm/dd/yyyy)" },
			{"585", "Mail Zip/DOB(mm/yyyy)" },
			{"590", "Home Zip/DOB(mm/yyyy)" },
			{"595", "Mail Zip/DOB(yyyy)" },
			{"600", "Home Zip/DOB(yyyy)" },
			{"605", "Mail Zip/DOB(yyy)" },
			{"610", "Home Zip/DOB(yyy)" },
			{"611", "Home Address/Home Zip/Phone" },
			{"612", "Home Address/Home Zip/Phone4" },
			{"613", "Home Zip/Phone" },
			{"614", "Home Zip/Phone4" },
			{"615", "Home Address/Phone" },
			{"616", "Home Address/Phone4" },
			{"621", "First Initial/Last/DOB(mm/yyyy)" },
			{"622", "First Initial/Last/DOB(yyyy)" },
			{"623", "First/Last/Home city/Home State/DOB(mm/dd/yyyy)" },
			{"624", "First/Last/Home city/Home State/DOB(mm/yyyy)" },
			{"625", "First/Last/Home city/Home State/DOB(yyyy)" },
			{"627", "First Initial/Last/Home city/Home State/DOB(mm/dd/yyyy)" },
			{"628", "First Initial/Last/Home city/Home State/DOB(mm/yyyy)" },
			{"629", "First Initial/Last/Home city/Home State/DOB(yyyy)" },
            {"630", "First/Last/Home city/DOB(mm/dd/yyyy)" },
			{"650", "First/Middle/Last/Home Address/Home city/Home State/Home Zip/DOB(mm/dd/yyyy)" },
			{"651", "First/Last/Home Address/Home city/Home State/Home Zip/DOB(mm/dd/yyyy)" },
			{"653", "First/Last/Home Address/Home city/Home State/Home Zip/DOB(mm/yyyy)" },
			{"655", "First/Last/Home Address/Home city/Home State/Home Zip/DOB(yyyy)" },
			{"696", "First/Last/Home Address/Home city/Home Zip/Phone" },
			{"697", "First Initial/Last/Home Address/Home city/Home Zip/Phone" },
			{"698", "First/Last/Home Address/Home city/Home Zip/Phone4" },
			{"699", "First Initial/Last/Home Address/Home city/Home Zip/Phone4" },
			{"700", "First/Last" },
			{"701", "First/Last/Gender" },
			{"702", "First/Last/Gender/ID" },
			{"703", "First/Last/Gender/Home Address/ID" },
			{"704", "First/Last/Gender/Home Address/Home Zip/ID" },
			{"705", "First/Last/ID" },
			{"706", "First/Last/Home Address" },
			{"707", "Last/Home Address" },
			{"708", "First/Last/Home Zip" },
			{"709", "First/Last/Home Zip/ID" },
			{"710", "Last/ID" },
			{"711", "Last/Home Zip/ID" },
			{"712", "Last/Home Zip/DOB(mm/dd/yyyy)" },
			{"713", "Last/Home Address/DOB(mm/dd/yyyy)" },
			{"714", "Last/Home Address/Home Zip/DOB(mm/dd/yyyy)" },
			{"715", "First/Last/Home city" },
			{"716", "First/Middle/Last" },
			{"717", "First/Middle/Last/Home city" },
			{"718", "First/Middle/Last/Home city/Home Zip" },
			{"719", "First/Middle/Last/Home city/Home State" },
			{"720", "First/Middle/Last/Home State" },
			{"721", "First/Middle/Last/Gender/Home Address/Home city/Home State/Home Zip/ID" },
			{"722", "First/Last/ID" },
			{"723", "First/Last/Phone" },
			{"724", "First/Last/Phone/DOB(mm/dd/yyyy)" },
			{"725", "First/Last/Home Zip/Phone" },
			{"726", "First/Last/Phone4" },
			{"727", "First/Last/Phone4/DOB(mm/dd/yyyy)" },
			{"728", "First/Last/Home Zip/Phone4" },
			{"729", "First Initial/Last/DOB(mm/dd/yyyy)" },
			{"730", "First/Last/DOB(mm/dd/yyyy)" },
			{"731", "First/Last/Home Address/Home Zip/Phone/DOB(mm/dd/yyyy)" },
			{"732", "First/Last/Home Address/Home Zip/Phone/DOB(mm/yyyy)" },
			{"733", "First/Last/Home Address/Home Zip/Phone/DOB(yyyy)" },
			{"734", "First/Last/Mail Address/Mail Zip/Phone/DOB(mm/dd/yyyy)" },
			{"735", "First/Last/Mail Address/Mail Zip/Phone/DOB(mm/yyyy)" },
			{"736", "First/Last/Mail Address/Mail Zip/Phone/DOB(yyyy)" },
			{"737", "First Initial/Last/Home Address/Home Zip/Phone/DOB(mm/dd/yyyy)" },
			{"738", "First Initial/Last/Home Address/Home Zip/Phone/DOB(mm/yyyy)" },
			{"739", "First Initial/Last/Home Address/Home Zip/Phone/DOB(yyyy)" },
			{"740", "First Initial/Last/Mail Address/Mail Zip/Phone/DOB(mm/dd/yyyy)" },
			{"741", "First Initial/Last/Mail Address/Mail Zip/Phone/DOB(mm/yyyy)" },
			{"742", "First Initial/Last/Mail Address/Mail Zip/Phone/DOB(yyyy)" },
			{"743", "First/Last/Home Address/Home Zip/Phone" },
			{"744", "First/Last/Mail Address/Mail Zip/Phone" },
			{"745", "First Initial/Last/Home Address/Home Zip/Phone" },
			{"746", "First Initial/Last/Mail Address/Mail Zip/Phone" },
			{"747", "First/Last/Home Zip/Phone/DOB(mm/dd/yyyy)" },
			{"748", "First/Last/Home Zip/Phone/DOB(mm/yyyy)" },
			{"749", "First/Last/Home Zip/Phone/DOB(yyyy)" },
			{"750", "First/Last/Mail Zip/Phone/DOB(mm/dd/yyyy)" },
			{"751", "First/Last/Mail Zip/Phone/DOB(mm/yyyy)" },
			{"752", "First/Last/Mail Zip/Phone/DOB(yyyy)" },
			{"753", "First Initial/Last/Home Zip/Phone/DOB(mm/dd/yyyy)" },
			{"754", "First Initial/Last/Home Zip/Phone/DOB(mm/yyyy)" },
			{"755", "First Initial/Last/Home Zip/Phone/DOB(yyyy)" },
			{"756", "First Initial/Last/Mail Zip/Phone/DOB(mm/dd/yyyy)" },
			{"757", "First Initial/Last/Mail Zip/Phone/DOB(mm/yyyy)" },
			{"758", "First Initial/Last/Mail Zip/Phone/DOB(yyyy)" },
			{"759", "First/Last/Home Address/Home Zip/Phone4/DOB(mm/dd/yyyy)" },
			{"760", "First/Last/Home Address/Home Zip/Phone4/DOB(mm/yyyy)" },
			{"761", "First/Last/Home Address/Home Zip/Phone4/DOB(yyyy)" },
			{"762", "First/Last/Mail Address/Mail Zip/Phone4/DOB(mm/dd/yyyy)" },
			{"763", "First/Last/Mail Address/Mail Zip/Phone4/DOB(mm/yyyy)" },
			{"764", "First/Last/Mail Address/Mail Zip/Phone4/DOB(yyyy)" },
			{"765", "First Initial/Last/Home Address/Home Zip/Phone4/DOB(mm/dd/yyyy)" },
			{"766", "First Initial/Last/Home Address/Home Zip/Phone4/DOB(mm/yyyy)" },
			{"767", "First Initial/Last/Home Address/Home Zip/Phone4/DOB(yyyy)" },
			{"768", "First Initial/Last/Mail Address/Mail Zip/Phone4/DOB(mm/dd/yyyy)" },
			{"769", "First Initial/Last/Mail Address/Mail Zip/Phone4/DOB(mm/yyyy)" },
			{"770", "First Initial/Last/Mail Address/Mail Zip/Phone4/DOB(yyyy)" },
			{"771", "First/Last/Home Address/Home Zip/Phone4" },
			{"772", "First/Last/Mail Address/Mail Zip/Phone4" },
			{"773", "First Initial/Last/Home Address/Home Zip/Phone4" },
			{"774", "First Initial/Last/Mail Address/Mail Zip/Phone4" },
			{"775", "First/Last/Home Zip/Phone4/DOB(mm/dd/yyyy)" },
			{"776", "First/Last/Home Zip/Phone4/DOB(mm/yyyy)" },
			{"777", "First/Last/Home Zip/Phone4/DOB(yyyy)" },
			{"778", "First/Last/Mail Zip/Phone4/DOB(mm/dd/yyyy)" },
			{"779", "First/Last/Mail Zip/Phone4/DOB(mm/yyyy)" },
			{"780", "First/Last/Mail Zip/Phone4/DOB(yyyy)" },
			{"781", "First Initial/Last/Home Zip/Phone4/DOB(mm/dd/yyyy)" },
			{"782", "First Initial/Last/Home Zip/Phone4/DOB(mm/yyyy)" },
			{"783", "First Initial/Last/Home Zip/Phone4/DOB(yyyy)" },
			{"784", "First Initial/Last/Mail Zip/Phone4/DOB(mm/dd/yyyy)" },
			{"785", "First Initial/Last/Mail Zip/Phone4/DOB(mm/yyyy)" },
			{"786", "First Initial/Last/Mail Zip/Phone4/DOB(yyyy)" },
			{"787", "Home State/Phone" },
			{"788", "Mail State/Phone" },
			{"789", "Phone" },
			{"790", "Last/Home State/Phone" },
			{"791", "Last/Mail State/Phone" },
			{"792", "Last/Home Address/Home Zip/Phone" },
			{"793", "Last/Home State/Phone4" },
			{"794", "Last/Mail State/Phone4" },
			{"795", "Last/mobile" },
			{"796", "First Initial/Last/Home Address" },
			{"797", "Last/Home Address/Phone" },
			{"798", "Last/Home Address/Phone4" },
			{"799", "Last/Phone" },
			{"800", "First/Last/Home Zip/DOB(mm/dd/yyyy)/SSN" },
			{"801", "First/Last/Home Zip/DOB(mm/yyyy)/SSN" },
			{"802", "First/Last/Home Zip/DOB(yyyy)/SSN" },
			{"803", "First/Last/Home Zip/SSN" },
			{"804", "First Initial/Last/Home State/DOB(mm/dd/yyyy)/SSN" },
			{"805", "First Initial/Last/Home State/DOB(mm/yyyy)/SSN" },
			{"806", "Last/Home State/DOB(mm/dd/yyyy)/SSN" },
			{"807", "First/Home State/DOB(mm/dd/yyyy)/SSN" },
			{"808", "Last/Home Zip/SSN" },
			{"809", "Last/Phone/SSN" },
			{"810", "Last/SSN" },
			{"811", "First/Last/SSN" },
			{"812", "Last/DOB(mm/dd/yyyy)/SSN" },
			{"813", "DOB(mm/dd/yyyy)/SSN" },
			{"814", "First/Last/Home Zip/DOB(mm/dd/yyyy)/SSN4" },
			{"815", "First/Last/Home Zip/DOB(mm/yyyy)/SSN4" },
			{"816", "First/Last/Home Zip/DOB(yyyy)/SSN4" },
			{"817", "First/Last/Home Zip/SSN4" },
			{"818", "First Initial/Last/Home State/DOB(mm/dd/yyyy)/SSN4" },
			{"819", "First Initial/Last/Home State/DOB(mm/yyyy)/SSN4" },
			{"820", "Last/Home State/DOB(mm/dd/yyyy)/SSN4" },
			{"821", "First/Home State/DOB(mm/dd/yyyy)/SSN4" },
			{"822", "Last/Home Zip/SSN4" },
			{"823", "Last/Phone/SSN4" },
			{"824", "Last/SSN4" },
			{"825", "First/Last/SSN4" },
			{"826", "Last/DOB(mm/dd/yyyy)/SSN4" },
			{"827", "DOB(mm/dd/yyyy)/SSN4" },
			{"828", "Last/Home Zip/DOB(mm/yyyy)/SSN4" },
			{"829", "First/Home Zip/DOB(mm/yyyy)/SSN4" },
			{"830", "Last/Gender/Home State/DOB(mm/yyyy)/SSN4" },
			{"831", "First/Gender/Home State/DOB(mm/yyyy)/SSN4" },
			{"832", "First/Last/Home State/DOB(mm/dd/yyyy)/SSN4" },
			{"833", "First/Last/Home State/DOB(mm/yyyy)/SSN4" },
			{"835", "Last/Home State/DOB(yyyy)/SSN" },
			{"836", "First Initial/Last/DOB(mm/dd/yyyy)/SSN" },
			{"837", "First Initial/Last/DOB(mm/dd/yyyy)/SSN4" },
			{"838", "First/Last/Home Address/Home Zip/DOB(mm/dd/yyyy)/SSN4" },
			{"839", "First/Last/Home Address/Home Zip/DOB(mm/yyyy)/SSN4" },
			{"840", "First/Last/Home Address/Home Zip/DOB(yyyy)/SSN4" },
			{"841", "First Initial/Last/Home Address/Home Zip/DOB(mm/dd/yyyy)/SSN4" },
			{"842", "First Initial/Last/Home Address/Home Zip/DOB(mm/yyyy)/SSN4" },
			{"843", "First Initial/Last/Home Address/Home Zip/DOB(yyyy)/SSN4" },
			{"844", "Last/Home Address/Home Zip/DOB(mm/dd/yyyy)/SSN4" },
			{"845", "Last/Home Address/Home Zip/DOB(mm/yyyy)/SSN4" },
			{"846", "Last/Home Address/Home Zip/DOB(yyyy)/SSN4" },
			{"847", "First Initial/Last/SSN4" },
			{"848", "First/Last/Home Address/Home Zip/SSN4" },
			{"849", "First Initial/Last/Home Address/Home Zip/SSN4" },
			{"850", "Last/Home Address/Home Zip/SSN4" },
			{"851", "First/Last/Home Address/Home city/Home State/Home Zip/Phone/DOB(mm/dd/yyyy)/SSN4" },
			{"852", "First/Last/Home Address/Home city/Home State/Home Zip/Phone/DOB(mm/yyyy)/SSN4" },
			{"853", "First/Last/Home Address/Home city/Home State/Home Zip/Phone/DOB(yyyy)/SSN4" },
			{"854", "First Initial/Last/Home Address/Home city/Home State/Home Zip/Phone/DOB(mm/dd/yyyy)/SSN4" },
			{"855", "First Initial/Last/Home Address/Home city/Home State/Home Zip/Phone/DOB(mm/yyyy)/SSN4" },
			{"856", "First Initial/Last/Home Address/Home city/Home State/Home Zip/Phone/DOB(yyyy)/SSN4" },
			{"877", "First/Last/Home Address/Home Zip/ID" },
			{"878", "First Initial/Last/Home Address/Home Zip/ID" },
			{"879", "Last/Home Address/Home Zip/ID" },
			{"880", "First/Last/Home State/ID" },
			{"881", "First/Last/Mail State/ID" },
			{"882", "First Initial/Last/Home State/ID" },
			{"883", "First Initial/Last/Mail State/ID" },
			{"884", "Last/Home State/ID" },
			{"885", "Last/Mail State/ID" },
			{"887", "DOB(mm/dd/yyyy)/ID" },
			{"890", "Mail State/ID" },
			{"895", "Home State/ID" },
			{"896", "ID#" },
			{"900", "ID Algorithm – DOB Verified" },
			{"905", "ID Algorithm – Format Verified" },
			{"1000", "First/Last/DOB(mm/dd/yyyy)/ID" },

			/* Supplemental Match Codes for the United Kingdom */
			{"3000", "Home Zip/Home Address/Last/First/DOB(mm/dd/yyyy)" },
			{"3001", "Home Zip/Home Address/Last/DOB(mm/dd/yyyy)" },
			{"3002", "Home Zip/Home Address/Last/First/18+" },
			{"3003", "Home Zip/Home Address/Last/First/DOB(mm/dd/yyyy)/HALO" },
			{"3004", "Home Zip/Home Address/Last/DOB(mm/dd/yyyy)/HALO" },
		};

		#endregion

		//#region ErrorCodes

		//public static string GetErrorDescription(string code)
		//{
		//	return !string.IsNullOrWhiteSpace(code) && ErrorCodes.ContainsKey(code) ? ErrorCodes[code] : string.Empty;
		//}

		//public static Dictionary<string, string> ErrorCodes = new Dictionary<string, string>
		//{
		//	{ "2001", "Missing Zip/Postal Code" },
		//	{ "2002", "No Account Found" },
		//	{ "2003", "Account Closed" },
		//	{ "2004", "Invalid DOB" },
		//	{ "2005", "Account Not Opened" },
		//	{ "2006", "Invalid Zip/Postal Code" },
		//	{ "2007", "Database Error Occurred" },
		//	{ "2008", "Missing State" },
		//	{ "2009", "DOB out of valid range for verification (i.e. 1/1/1995)" },
		//	{ "2010", "DOB before 1900" },
		//	{ "2011", "Missing or Invalid Verification Data" },
		//	{ "2012", "Missing DOB" },
		//	{ "2013", "Zip/State do not match" },
		//	{ "2015", "Must provide Zip or State" },
		//	{ "2016", "XX: Country Not Supported" },
		//	{ "2017", "ID# Not Submitted" },
		//	{ "2018", "ID Required for this Country" },
		//	{ "2030", "VME is not available" },
		//	{ "2031", "Invalid IP" },
		//	{ "2032", "Invalid ID" },
		//	{ "2050", "Missing or Invalid First Name" },
		//	{ "2051", "Missing or Invalid Last Name" },
		//	{ "2052", "Missing or Invalid Address" },
		//	{ "2070", "Missing or Invalid Contact Phone" },
		//	{ "2071", "Missing or Invalid Contact Email" },
		//	{ "2072", "Missing or Invalid Contact Amount" },
		//	{ "2073", "Missing or Invalid Phone" },
		//	{ "2074", "Missing or Invalid Social Security No." },
		//	{ "2079", "Missing or Invalid Mobile Number" },
		//	{ "2080", "Missing or Invalid County" },
		//	{ "2082", "Missing or Invalid User IP Address" },
		//	{ "2999", "Invalid Acct Type" },
		//	{ "2110", "E-Sig Failure" },
		//	{ "4000", "Provider Error" },
		//	{ "4001", "Provider Timeout" },
		//};

		//#endregion
	}
}
