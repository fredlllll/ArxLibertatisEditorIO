using ArxLibertatisEditorIO.RawIO.FTL;
using ArxLibertatisEditorIO.Util;
using System.Collections.Generic;

namespace ArxLibertatisEditorIO.MediumIO.FTL
{
    public class DataSection3D
    {
        public readonly DataSection3DHeader header = new DataSection3DHeader();

        public readonly List<Vertex> vertexList = new List<Vertex>();
        public readonly List<Face> faceList = new List<Face>();
        public readonly List<string> textures = new List<string>();
        public readonly List<Group> groups = new List<Group>();
        public readonly List<ActionList> actions = new List<ActionList>();
        public readonly List<Selection> selections = new List<Selection>();

        public void LoadFrom(FTL_IO_3D_DATA_SECTION _3DDataSection)
        {
            header.LoadFrom(ref _3DDataSection.header);

            vertexList.Clear();
            for (int i = 0; i < _3DDataSection.header.nb_vertex; ++i)
            {
                var vert = new Vertex();
                vert.LoadFrom(ref _3DDataSection.vertexList[i]);
                vertexList.Add(vert);
            }

            faceList.Clear();
            for (int i = 0; i < _3DDataSection.faceList.Length; ++i)
            {
                var face = new Face();
                face.LoadFrom(ref _3DDataSection.faceList[i]);
                faceList.Add(face);
            }

            textures.Clear();
            for (int i = 0; i < _3DDataSection.textureContainers.Length; ++i)
            {
                textures.Add(IOHelper.GetString(_3DDataSection.textureContainers[i].name));
            }

            groups.Clear();
            for (int i = 0; i < _3DDataSection.groups.Length; ++i)
            {
                var group = new Group();
                group.LoadFrom(ref _3DDataSection.groups[i]);
                groups.Add(group);
            }

            actions.Clear();
            for (int i = 0; i < _3DDataSection.actionList.Length; ++i)
            {
                var list = new ActionList();
                list.LoadFrom(_3DDataSection.actionList[i]);
                actions.Add(list);
            }

            selections.Clear();
            for (int i = 0; i < _3DDataSection.selections.Length; ++i)
            {
                var selection = new Selection();
                selection.LoadFrom(ref _3DDataSection.selections[i]);
                selections.Add(selection);
            }
        }

        public void SaveTo(FTL_IO_3D_DATA_SECTION _3DDataSection)
        {
            header.SaveTo(ref _3DDataSection.header);

            _3DDataSection.header.nb_vertex = vertexList.Count;
            _3DDataSection.header.nb_faces = faceList.Count;
            _3DDataSection.header.nb_maps = textures.Count;
            _3DDataSection.header.nb_groups = groups.Count;
            _3DDataSection.header.nb_action = actions.Count;
            _3DDataSection.header.nb_selections = selections.Count;

            IOHelper.EnsureArraySize(ref _3DDataSection.vertexList, vertexList.Count);
            for (int i = 0; i < vertexList.Count; ++i)
            {
                vertexList[i].SaveTo(ref _3DDataSection.vertexList[i]);
            }

            IOHelper.EnsureArraySize(ref _3DDataSection.faceList, faceList.Count);
            for (int i = 0; i < faceList.Count; ++i)
            {
                faceList[i].SaveTo(ref _3DDataSection.faceList[i]);
            }

            IOHelper.EnsureArraySize(ref _3DDataSection.textureContainers, textures.Count);
            for (int i = 0; i < textures.Count; ++i)
            {
                _3DDataSection.textureContainers[i].name = IOHelper.GetBytes(textures[i], 256);
            }

            IOHelper.EnsureArraySize(ref _3DDataSection.groups, groups.Count);
            for (int i = 0; i < groups.Count; ++i)
            {
                groups[i].SaveTo(ref _3DDataSection.groups[i]);
            }

            IOHelper.EnsureArraySize(ref _3DDataSection.actionList, actions.Count);
            for (int i = 0; i < groups.Count; ++i)
            {
                actions[i].SaveTo(ref _3DDataSection.actionList[i]);
            }

            IOHelper.EnsureArraySize(ref _3DDataSection.selections, selections.Count);
            for (int i = 0; i < selections.Count; ++i)
            {
                selections[i].SaveTo(ref _3DDataSection.selections[i]);
            }
        }
    }
}