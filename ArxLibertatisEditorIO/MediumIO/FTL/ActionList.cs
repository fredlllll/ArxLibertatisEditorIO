﻿using ArxLibertatisEditorIO.RawIO.FTL;
using ArxLibertatisEditorIO.Util;

namespace ArxLibertatisEditorIO.MediumIO.FTL
{
    public class ActionList
    {
        public string name;
        public int indexVertex;
        public int action;
        public int sfx;

        public void LoadFrom(EERIE_ACTIONLIST_FTL action)
        {
            name = IOHelper.GetString(action.name);
            indexVertex = action.idx;
            this.action = action.action;
            sfx = action.sfx;
        }

        public void SaveTo(ref EERIE_ACTIONLIST_FTL action)
        {
            action.name = IOHelper.GetBytes(name, 256);
            action.idx = indexVertex;
            action.action = this.action;
            action.sfx = sfx;
        }
    }
}