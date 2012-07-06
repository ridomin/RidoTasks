using System;
namespace trx2html.Parser
{
    interface I3ValueBar
    {
        double PercentIgnored { get; }
        double PercentKO { get; }
        double PercentOK { get; }
    }
}
