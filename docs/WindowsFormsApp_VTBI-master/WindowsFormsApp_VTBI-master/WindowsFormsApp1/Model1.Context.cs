﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré à partir d'un modèle.
//
//     Des modifications manuelles apportées à ce fichier peuvent conduire à un comportement inattendu de votre application.
//     Les modifications manuelles apportées à ce fichier sont remplacées si le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WindowsFormsApp1
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Model1Container : DbContext
    {
        public Model1Container()
            : base("name=Model1Container")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Evenement> EvenementSet { get; set; }
        public virtual DbSet<Essai> EssaiSet { get; set; }
        public virtual DbSet<SuiviValeur> SuiviValeurSet { get; set; }
        public virtual DbSet<Trade> TradeSet { get; set; }
        public virtual DbSet<SuiviSignal> SuiviSignalSet { get; set; }
        public virtual DbSet<Methode> MethodeSet { get; set; }
        public virtual DbSet<MethodeSuivi> MethodeSuiviSet { get; set; }
        public virtual DbSet<MethodeSortie> MethodeSortieSet { get; set; }
    }
}
