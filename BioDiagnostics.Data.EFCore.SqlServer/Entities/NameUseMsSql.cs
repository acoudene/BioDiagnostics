using System.ComponentModel;

namespace BioDiagnostics.Data.EFCore.SqlServer.Entities;

public enum NameUseMsSql
{
  //
  // Summary:
  //     Known as/conventional/the one you normally use. (system: http://hl7.org/fhir/name-use)
  [Description("Usual")]
  Usual,
  //
  // Summary:
  //     The formal name as registered in an official (government) registry, but which
  //     name might not be commonly used. May be called \"legal name\". (system: http://hl7.org/fhir/name-use)
  [Description("Official")]
  Official,
  //
  // Summary:
  //     A temporary name. Name.period can provide more detailed information. This may
  //     also be used for temporary names assigned at birth or in emergency situations.
  //     (system: http://hl7.org/fhir/name-use)
  [Description("Temp")]
  Temp,
  //
  // Summary:
  //     A name that is used to address the person in an informal manner, but is not part
  //     of their formal or usual name. (system: http://hl7.org/fhir/name-use)
  [Description("Nickname")]
  Nickname,
  //
  // Summary:
  //     Anonymous assigned name, alias, or pseudonym (used to protect a person's identity
  //     for privacy reasons). (system: http://hl7.org/fhir/name-use)
  [Description("Anonymous")]
  Anonymous,
  //
  // Summary:
  //     This name is no longer in use (or was never correct, but retained for records).
  //     (system: http://hl7.org/fhir/name-use)
  [Description("Old")]
  Old,
  //
  // Summary:
  //     A name used prior to changing name because of marriage. This name use is for
  //     use by applications that collect and store names that were used prior to a marriage.
  //     Marriage naming customs vary greatly around the world, and are constantly changing.
  //     This term is not gender specific. The use of this term does not imply any particular
  //     history for a person's name. (system: http://hl7.org/fhir/name-use)
  [Description("Name changed for Marriage")]
  Maiden
}