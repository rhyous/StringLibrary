using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Rhyous.StringLibrary.Pluralization
{
    /// <summary>
    /// A dictionary to hold custom pluralizations in English.
    /// If you see a noun missing, don't hesitate to ask for it to be 
    /// added here: https://github.com/rhyous/StringLibrary
    /// Either: 1. Fork, fix, request a pull request, or 
    ///         2. Submit an Issue.
    /// </summary>
    /// <remarks>By re-implementing IDictionary, I can change how the
    /// Add method works. Concurrent dictionary calls TryAdd, but I 
    /// wanted to call AddOrUpdate.</remarks>
    public class USEnglishPluralizationDictionary : ConcurrentDictionary<string, string>
                                                  , IDictionary<string,string>
                                                  , IDictionary
    {
        public USEnglishPluralizationDictionary() : base(StringComparer.OrdinalIgnoreCase)
        {
            Init();
        }

        public void Add(string key, string value)
        {
            AddOrUpdate(key, value, (k, v) => value);
        }

        protected virtual void Init()
        {
            Add("addendum", "addenda");
            Add("aircraft", "aircraft");
            Add("alumna", "alumnae");
            Add("alumnus", "alumni");
            Add("analysis", "analyses");
            Add("antenna", "antennae");
            Add("antithesis", "antitheses");
            Add("appendix", "appendices");
            Add("armor", "armor");
            Add("axis", "axes");
            Add("bacillus", "bacilli");
            Add("bacterium", "bacteria");
            Add("basis", "bases");
            Add("bison", "bison");
            Add("buffalo", "buffalo");
            Add("carp", "carp");
            Add("cod", "cod");
            Add("cactus", "cacti");
            Add("canto", "cantos");
            Add("child", "children");
            Add("codex", "codices");
            Add("corpus", "corpora");
            Add("crisis", "crises");
            Add("criterion", "criteria");
            Add("curriculum", "curricula");
            Add("datum", "data");
            Add("deer", "deer");
            Add("diagnosis", "diagnoses");
            Add("die", "dice");
            Add("dwarf", "dwarves");
            Add("echo", "echoes");
            Add("elf", "elves");
            Add("ellipsis", "ellipses");
            Add("erratum", "errata");
            Add("fish", "fish");
            Add("focus", "foci");
            Add("foot", "feet");
            Add("fungus", "fungi");
            Add("genus", "genera");
            Add("goose", "geese");
            Add("graffito", "graffiti");
            Add("grouse", "grouse");
            Add("half", "halves");
            Add("hetero", "heteros");
            Add("hoof", "hooves");
            Add("hypothesis", "hypotheses");
            Add("index", "indices");
            Add("kimono", "kimonos");
            Add("larva", "larvae");
            Add("libretto", "libretti");
            Add("life", "lives");
            Add("loaf", "loaves");
            Add("locus", "loci");
            Add("louse", "lice");
            Add("man", "men");
            Add("matrix", "matrices");
            Add("medium", "media");
            Add("memorandum", "memoranda");
            Add("minutia", "minutiae");
            Add("moose", "moose");
            Add("mouse", "mice");
            Add("nebula", "nebulae");
            Add("nucleus", "nuclei");
            Add("oasis", "oases");
            Add("offspring", "offspring");
            Add("opus", "opera");
            Add("ovum", "ova");
            Add("ox", "oxen");
            Add("parenthesis", "parentheses");
            Add("phenomenon", "phenomena");
            Add("photo", "photos");
            Add("phylum", "phyla");
            Add("piano", "pianos");
            Add("portico", "porticos");
            Add("pro", "pros");
            Add("prognosis", "prognoses");
            Add("quarto", "quartos");
            Add("quiz", "quizzes");
            Add("radius", "radii");
            Add("referendum", "referenda");
            Add("salmon", "salmon");
            Add("scarf", "scarves");
            Add("self", "selves");
            Add("series", "series");
            Add("sheep", "sheep");
            Add("shrimp", "shrimp");
            Add("species", "species");
            Add("stimulus", "stimuli");
            Add("stomach", "stomachs");
            Add("stratum", "strata");
            Add("swine", "swine");
            Add("syllabus", "syllabi");
            Add("symposium", "symposia");
            Add("synopsis", "synopses");
            Add("tableau", "tableaus");
            Add("thesis", "theses");
            Add("thief", "thieves");
            Add("tooth", "teeth");
            Add("trout", "trout");
            Add("tuna", "tuna");
            Add("vertebra", "vertebrae");
            Add("vertex", "vertices");
            Add("veto", "vetos");
            Add("vita", "vitae");
            Add("vortex", "vortices");
            Add("wharf", "wharves");
            Add("wife", "wives");
            Add("wolf", "wolves");
            Add("woman", "women");
            Add("zero", "zeros");
        }
    }
}