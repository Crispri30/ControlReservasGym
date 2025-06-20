USE [ControlGym]
GO
/****** Object:  Table [dbo].[Accesos]    Script Date: 6/06/2025 2:56:16 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Accesos](
	[AccesoID] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioID] [int] NULL,
	[FechaAcceso] [datetime2](7) NULL,
	[ClaseID] [int] NULL,
	[EstadoAcceso] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[AccesoID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Clases]    Script Date: 6/06/2025 2:56:16 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clases](
	[ClaseID] [int] IDENTITY(1,1) NOT NULL,
	[NombreClase] [nvarchar](100) NOT NULL,
	[FechaHoraInicio] [datetime2](7) NULL,
	[FechaHoraFin] [datetime2](7) NULL,
	[Duracion] [int] NULL,
	[CapacidadMaxima] [int] NULL,
	[EntrenadorID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ClaseID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Entrenadores]    Script Date: 6/06/2025 2:56:16 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Entrenadores](
	[EntrenadorID] [int] NOT NULL,
	[Nombre] [nvarchar](100) NULL,
	[Especialidad] [nvarchar](100) NULL,
	[Disponibilidad] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[EntrenadorID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reservas]    Script Date: 6/06/2025 2:56:16 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservas](
	[ReservaID] [int] IDENTITY(1,1) NOT NULL,
	[UsuarioID] [int] NULL,
	[ClaseID] [int] NULL,
	[FechaReserva] [datetime2](7) NULL,
	[Estado] [bit] NULL,
	[FechaCancelacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[ReservaID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 6/06/2025 2:56:16 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[UsuarioID] [int] NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Email] [nvarchar](100) NULL,
	[Telefono] [nvarchar](100) NULL,
	[FechaVencimientoMembresia] [datetime] NOT NULL,
	[TipoMembresia] [nvarchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[UsuarioID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Accesos]  WITH CHECK ADD FOREIGN KEY([ClaseID])
REFERENCES [dbo].[Clases] ([ClaseID])
GO
ALTER TABLE [dbo].[Accesos]  WITH CHECK ADD FOREIGN KEY([UsuarioID])
REFERENCES [dbo].[Usuarios] ([UsuarioID])
GO
ALTER TABLE [dbo].[Clases]  WITH CHECK ADD FOREIGN KEY([EntrenadorID])
REFERENCES [dbo].[Entrenadores] ([EntrenadorID])
GO
ALTER TABLE [dbo].[Reservas]  WITH CHECK ADD FOREIGN KEY([ClaseID])
REFERENCES [dbo].[Clases] ([ClaseID])
GO
ALTER TABLE [dbo].[Reservas]  WITH CHECK ADD FOREIGN KEY([UsuarioID])
REFERENCES [dbo].[Usuarios] ([UsuarioID])
GO
