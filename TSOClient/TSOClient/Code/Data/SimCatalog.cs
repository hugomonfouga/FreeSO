﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using TSOClient.VM;
using SimsLib.ThreeD;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using TSOClient.Code.Rendering.Sim;

namespace TSOClient.Code.Data
{
    /// <summary>
    /// Place to get information and assets related to sims, e.g. skins, thumbnails etc
    /// </summary>
    public class SimCatalog
    {
        public static void GetCollection(ulong fileID)
        {
            var collectionData = ContentManager.GetResourceFromLongID(fileID);
            var reader = new BinaryReader(new MemoryStream(collectionData));
        }

        public SimCatalog()
        {
        }

        private static Dictionary<ulong, Binding> Bindings = new Dictionary<ulong, Binding>();
        public static Binding GetBinding(ulong id)
        {
            if (Bindings.ContainsKey(id))
            {
                return Bindings[id];
            }

            var bytes = ContentManager.GetResourceFromLongID(id);
            var binding = new Binding(bytes);
            Bindings.Add(id, binding);
            return binding;
        }

        private static Dictionary<ulong, Appearance> Appearances = new Dictionary<ulong, Appearance>();
        public static Appearance GetAppearance(ulong id)
        {
            if (Appearances.ContainsKey(id))
            {
                return Appearances[id];
            }

            var bytes = ContentManager.GetResourceFromLongID(id);
            var app = new Appearance(bytes);
            Appearances.Add(id, app);
            return app;
        }

        private static Dictionary<ulong, Outfit> Outfits = new Dictionary<ulong, Outfit>();
        public static Outfit GetOutfit(ulong id)
        {
            if (Outfits.ContainsKey(id))
            {
                return Outfits[id];
            }

            var bytes = ContentManager.GetResourceFromLongID(id);
            var outfit = new Outfit(bytes);
            Outfits.Add(id, outfit);
            return outfit;
        }

        private static Dictionary<ulong, Texture2D> OutfitTextures = new Dictionary<ulong, Texture2D>();
        public static Texture2D GetOutfitTexture(ulong id)
        {
            if (OutfitTextures.ContainsKey(id))
            {
                return OutfitTextures[id];
            }

            var bytes = ContentManager.GetResourceFromLongID(id);
            using (var stream = new MemoryStream(bytes))
            {
                var texture = Texture2D.FromFile(GameFacade.GraphicsDevice, stream);
                OutfitTextures.Add(id, texture);
                return texture;
            }
        }


        private static Dictionary<ulong, Mesh> OutfitMeshes = new Dictionary<ulong, Mesh>();
        public static Mesh GetOutfitMesh(ulong id)
        {
            if (OutfitMeshes.ContainsKey(id))
            {
                return OutfitMeshes[id].Clone();
            }

            var mesh = new Mesh();
            mesh.Read(ContentManager.GetResourceFromLongID(id));
            mesh.ProcessMesh();
            OutfitMeshes.Add(id, mesh);
            return mesh;
        }

        public static Mesh GetOutfitMesh(Skeleton Skel, ulong id)
        {
            if (OutfitMeshes.ContainsKey(id))
            {
                return OutfitMeshes[id].Clone();
            }
            
            var mesh = new Mesh();
            mesh.Read(ContentManager.GetResourceFromLongID(id));
            mesh.ProcessMesh();
            OutfitMeshes.Add(id, mesh);
            return mesh;
        }

        public static void LoadSim3D(Sim sim)
        {
            LoadSimHead(sim);
            LoadSimBody(sim);

            sim.Reposition();
            
            //var headOutfit = SimCatalog.GetOutfit(sim.HeadOutfitID);
            //var headAppearance = headOutfit.GetAppearance(sim.Appearance);

            //var Apr = new Appearance(ContentManager.GetResourceFromLongID(OutfHead.GetAppearance(skin)));
            //var Bnd = new Binding(ContentManager.GetResourceFromLongID(Apr.BindingIDs[0]));

            //sim.HeadTexture = GetOutfitTexture(Bnd.TextureAssetID);
            //sim.HeadMesh = GetOutfitMesh(sim.SimSkeleton, Bnd.MeshAssetID);
        }

        public static void LoadSimHead(Sim sim)
        {
            var outfit = SimCatalog.GetOutfit(sim.HeadOutfitID)
                            .GetAppearanceObject(sim.AppearanceType);

            sim.HeadBindings = outfit.BindingIDs.Select(
                x => new SimModelBinding(x)
            ).ToList();
        }

        public static void LoadSimBody(Sim sim)
        {
            var outfit = SimCatalog.GetOutfit(sim.BodyOutfitID)
                            .GetAppearanceObject(sim.AppearanceType);


            sim.BodyBindings = outfit.BindingIDs.Select(
                x => new SimModelBinding(x)
            ).ToList();
        }
    }

    public static class SimsLibExtensions
    {
        public static Appearance GetAppearanceObject(this Outfit outfit, AppearanceType type)
        {
            return SimCatalog.GetAppearance(outfit.GetAppearance(type));
        }

        public static Binding GetBinding(this Appearance appearance)
        {
            return SimCatalog.GetBinding(appearance.BindingIDs[0]);
        }

        public static Mesh LoadMesh(this Binding binding)
        {
            return SimCatalog.GetOutfitMesh(binding.MeshAssetID);
        }

        public static Texture2D LoadTexture(this Binding binding)
        {
            return SimCatalog.GetOutfitTexture(binding.TextureAssetID);
        }
    }



}