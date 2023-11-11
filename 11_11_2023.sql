PGDMP  9    4            
    {            Viking.Sports    16.0 (Debian 16.0-1.pgdg120+1)    16.0     3           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            4           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            5           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            6           1262    40965    Viking.Sports    DATABASE     z   CREATE DATABASE "Viking.Sports" WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'en_US.utf8';
    DROP DATABASE "Viking.Sports";
                Viking    false            �            1259    40992    ExerciseDictionary    TABLE     Z   CREATE TABLE public."ExerciseDictionary" (
    "Code" bigint NOT NULL,
    "Name" text
);
 (   DROP TABLE public."ExerciseDictionary";
       public         heap    Viking    false            �            1259    40971 	   Exercises    TABLE     }   CREATE TABLE public."Exercises" (
    "Id" uuid NOT NULL,
    "IdWorkout" uuid NOT NULL,
    "ExerciseName" text NOT NULL
);
    DROP TABLE public."Exercises";
       public         heap    Viking    false            �            1259    40978    Sets    TABLE     �   CREATE TABLE public."Sets" (
    "Id" uuid NOT NULL,
    "IdExercise" uuid NOT NULL,
    "Number" bigint NOT NULL,
    "SetWeight" bigint NOT NULL,
    "RepetitionNuber" smallint,
    "LapsTime" bigint
);
    DROP TABLE public."Sets";
       public         heap    Viking    false            �            1259    40966    Workout    TABLE     �   CREATE TABLE public."Workout" (
    "Id" uuid NOT NULL,
    "IdUser" uuid NOT NULL,
    "WorkoutName" text NOT NULL,
    "DateOfWeek" timestamp with time zone NOT NULL
);
    DROP TABLE public."Workout";
       public         heap    Viking    false            �            1259    40985    WorkoutDictionary    TABLE     b   CREATE TABLE public."WorkoutDictionary" (
    "Code" bigint NOT NULL,
    "Name" text NOT NULL
);
 '   DROP TABLE public."WorkoutDictionary";
       public         heap    Viking    false            0          0    40992    ExerciseDictionary 
   TABLE DATA           >   COPY public."ExerciseDictionary" ("Code", "Name") FROM stdin;
    public          Viking    false    219   �       -          0    40971 	   Exercises 
   TABLE DATA           H   COPY public."Exercises" ("Id", "IdWorkout", "ExerciseName") FROM stdin;
    public          Viking    false    216   �       .          0    40978    Sets 
   TABLE DATA           j   COPY public."Sets" ("Id", "IdExercise", "Number", "SetWeight", "RepetitionNuber", "LapsTime") FROM stdin;
    public          Viking    false    217   �       ,          0    40966    Workout 
   TABLE DATA           P   COPY public."Workout" ("Id", "IdUser", "WorkoutName", "DateOfWeek") FROM stdin;
    public          Viking    false    215   �       /          0    40985    WorkoutDictionary 
   TABLE DATA           =   COPY public."WorkoutDictionary" ("Code", "Name") FROM stdin;
    public          Viking    false    218          �           2606    40998 *   ExerciseDictionary ExerciseDictionary_pkey 
   CONSTRAINT     p   ALTER TABLE ONLY public."ExerciseDictionary"
    ADD CONSTRAINT "ExerciseDictionary_pkey" PRIMARY KEY ("Code");
 X   ALTER TABLE ONLY public."ExerciseDictionary" DROP CONSTRAINT "ExerciseDictionary_pkey";
       public            Viking    false    219            �           2606    40977    Exercises Exercises_pkey 
   CONSTRAINT     \   ALTER TABLE ONLY public."Exercises"
    ADD CONSTRAINT "Exercises_pkey" PRIMARY KEY ("Id");
 F   ALTER TABLE ONLY public."Exercises" DROP CONSTRAINT "Exercises_pkey";
       public            Viking    false    216            �           2606    40982    Sets Sets_pkey 
   CONSTRAINT     R   ALTER TABLE ONLY public."Sets"
    ADD CONSTRAINT "Sets_pkey" PRIMARY KEY ("Id");
 <   ALTER TABLE ONLY public."Sets" DROP CONSTRAINT "Sets_pkey";
       public            Viking    false    217            �           2606    40991 (   WorkoutDictionary WorkoutDictionary_pkey 
   CONSTRAINT     n   ALTER TABLE ONLY public."WorkoutDictionary"
    ADD CONSTRAINT "WorkoutDictionary_pkey" PRIMARY KEY ("Code");
 V   ALTER TABLE ONLY public."WorkoutDictionary" DROP CONSTRAINT "WorkoutDictionary_pkey";
       public            Viking    false    218            �           2606    40970    Workout Workout_pkey 
   CONSTRAINT     X   ALTER TABLE ONLY public."Workout"
    ADD CONSTRAINT "Workout_pkey" PRIMARY KEY ("Id");
 B   ALTER TABLE ONLY public."Workout" DROP CONSTRAINT "Workout_pkey";
       public            Viking    false    215            �           2606    49179    Exercises FK_exercise_id    FK CONSTRAINT     �   ALTER TABLE ONLY public."Exercises"
    ADD CONSTRAINT "FK_exercise_id" FOREIGN KEY ("IdWorkout") REFERENCES public."Workout"("Id") ON UPDATE RESTRICT ON DELETE RESTRICT;
 F   ALTER TABLE ONLY public."Exercises" DROP CONSTRAINT "FK_exercise_id";
       public          Viking    false    215    216    3219            0      x������ � �      -   �   x�����0�I4���q���@�z@HA\�6�"r�IO�A��e(QDt�f	�tj	�c}Z�l`��Z�BD#�^8���_��~��~��������۬�X�9ѐB"�`L�ղ�1��UT P� EVH56��e1��ǔ.�y����|J      .      x������ � �      ,   d   x�˱�0�Z�"}@���(ɳ��Ms�D���|`�hPȭd�-�$��Z.��R�%�t<�lh�ᔗ#�sx+o����#�D?��ֿ��w�Z�w��      /      x������ � �     