--
-- PostgreSQL database dump
--

-- Dumped from database version 16.2
-- Dumped by pg_dump version 16.2

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: circlegeofence; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.circlegeofence (
    id bigint NOT NULL,
    geofenceid bigint,
    radius bigint,
    latitude real,
    longitude real
);


ALTER TABLE public.circlegeofence OWNER TO postgres;

--
-- Name: circlegeofence_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.circlegeofence_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.circlegeofence_id_seq OWNER TO postgres;

--
-- Name: circlegeofence_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.circlegeofence_id_seq OWNED BY public.circlegeofence.id;


--
-- Name: driver; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.driver (
    driverid bigint NOT NULL,
    drivername character varying,
    phonenumber bigint
);


ALTER TABLE public.driver OWNER TO postgres;

--
-- Name: drivers_driverid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.drivers_driverid_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.drivers_driverid_seq OWNER TO postgres;

--
-- Name: drivers_driverid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.drivers_driverid_seq OWNED BY public.driver.driverid;


--
-- Name: geofences; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.geofences (
    geofenceid bigint NOT NULL,
    geofencetype character varying,
    addeddate bigint,
    strockcolor character varying,
    strockopacity real,
    strockweight real,
    fillcolor character varying,
    fillopacity real
);


ALTER TABLE public.geofences OWNER TO postgres;

--
-- Name: geofences_geofenceid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.geofences_geofenceid_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.geofences_geofenceid_seq OWNER TO postgres;

--
-- Name: geofences_geofenceid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.geofences_geofenceid_seq OWNED BY public.geofences.geofenceid;


--
-- Name: polygongeofence; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.polygongeofence (
    id bigint NOT NULL,
    geofenceid bigint,
    latitude real,
    longitude real
);


ALTER TABLE public.polygongeofence OWNER TO postgres;

--
-- Name: polygongeofence_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.polygongeofence_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.polygongeofence_id_seq OWNER TO postgres;

--
-- Name: polygongeofence_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.polygongeofence_id_seq OWNED BY public.polygongeofence.id;


--
-- Name: rectanglegeofence; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.rectanglegeofence (
    id bigint NOT NULL,
    geofenceid bigint,
    north real,
    east real,
    west real,
    south real
);


ALTER TABLE public.rectanglegeofence OWNER TO postgres;

--
-- Name: rectanglegeofence_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.rectanglegeofence_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.rectanglegeofence_id_seq OWNER TO postgres;

--
-- Name: rectanglegeofence_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.rectanglegeofence_id_seq OWNED BY public.rectanglegeofence.id;


--
-- Name: routehistory; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.routehistory (
    routehistoryid bigint NOT NULL,
    vehicleid bigint,
    vehicledirection integer,
    status character(1),
    vehiclespeed character varying,
    address character varying,
    latitude real,
    longitude real,
    recordtime bigint
);


ALTER TABLE public.routehistory OWNER TO postgres;

--
-- Name: routehistory_routehistoryid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.routehistory_routehistoryid_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.routehistory_routehistoryid_seq OWNER TO postgres;

--
-- Name: routehistory_routehistoryid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.routehistory_routehistoryid_seq OWNED BY public.routehistory.routehistoryid;


--
-- Name: vehicles; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.vehicles (
    vehicleid bigint NOT NULL,
    vehiclenumber bigint,
    vehicletype character varying
);


ALTER TABLE public.vehicles OWNER TO postgres;

--
-- Name: vehicles_vehicleid_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.vehicles_vehicleid_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.vehicles_vehicleid_seq OWNER TO postgres;

--
-- Name: vehicles_vehicleid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.vehicles_vehicleid_seq OWNED BY public.vehicles.vehicleid;


--
-- Name: vehiclesinformations; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.vehiclesinformations (
    id bigint NOT NULL,
    vehicleid bigint,
    driverid bigint,
    vehiclemake character varying,
    vehiclemodel character varying,
    purchasedate bigint
);


ALTER TABLE public.vehiclesinformations OWNER TO postgres;

--
-- Name: vehiclesinformations_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.vehiclesinformations_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.vehiclesinformations_id_seq OWNER TO postgres;

--
-- Name: vehiclesinformations_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.vehiclesinformations_id_seq OWNED BY public.vehiclesinformations.id;


--
-- Name: circlegeofence id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.circlegeofence ALTER COLUMN id SET DEFAULT nextval('public.circlegeofence_id_seq'::regclass);


--
-- Name: driver driverid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.driver ALTER COLUMN driverid SET DEFAULT nextval('public.drivers_driverid_seq'::regclass);


--
-- Name: geofences geofenceid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.geofences ALTER COLUMN geofenceid SET DEFAULT nextval('public.geofences_geofenceid_seq'::regclass);


--
-- Name: polygongeofence id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.polygongeofence ALTER COLUMN id SET DEFAULT nextval('public.polygongeofence_id_seq'::regclass);


--
-- Name: rectanglegeofence id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.rectanglegeofence ALTER COLUMN id SET DEFAULT nextval('public.rectanglegeofence_id_seq'::regclass);


--
-- Name: routehistory routehistoryid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.routehistory ALTER COLUMN routehistoryid SET DEFAULT nextval('public.routehistory_routehistoryid_seq'::regclass);


--
-- Name: vehicles vehicleid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.vehicles ALTER COLUMN vehicleid SET DEFAULT nextval('public.vehicles_vehicleid_seq'::regclass);


--
-- Name: vehiclesinformations id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.vehiclesinformations ALTER COLUMN id SET DEFAULT nextval('public.vehiclesinformations_id_seq'::regclass);


--
-- Data for Name: circlegeofence; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.circlegeofence (id, geofenceid, radius, latitude, longitude) FROM stdin;
9	6	1000	40.7128	-74.006
10	9	2000	40.759	-73.9845
11	12	3000	40.7711	-73.9677
12	15	4000	40.7587	-73.9753
\.


--
-- Data for Name: driver; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.driver (driverid, drivername, phonenumber) FROM stdin;
1	John Doe	1234567890
2	Jane Smith	2345678901
3	Bob Johnson	3456789012
4	Alice Williams	4567890123
5	Charlie Brown	5678901234
6	Osama	588888888
\.


--
-- Data for Name: geofences; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.geofences (geofenceid, geofencetype, addeddate, strockcolor, strockopacity, strockweight, fillcolor, fillopacity) FROM stdin;
6	Circle	1614556800	#FF0000	0.8	2	#00FF00	0.6
7	Rectangle	1614643200	#00FF00	0.7	2.5	#0000FF	0.7
8	Polygon	1614729600	#0000FF	0.6	3	#FF0000	0.8
9	Circle	1614816000	#FFFF00	0.5	3.5	#00FFFF	0.9
10	Rectangle	1614902400	#FF00FF	0.4	4	#FFFF00	1
11	Polygon	1614988800	#00FFFF	0.3	4.5	#FF00FF	1.1
12	Circle	1615075200	#FF00FF	0.2	5	#FFFF00	1.2
13	Rectangle	1615161600	#FFFF00	0.1	5.5	#00FFFF	1.3
14	Polygon	1615248000	#00FFFF	0	6	#FF00FF	1.4
15	Circle	1615334400	#FF00FF	0.9	6.5	#FFFF00	1.5
16	Rectangle	1615420800	#FFFF00	0.8	7	#00FFFF	1.6
17	Polygon	1615507200	#00FFFF	0.7	7.5	#FF00FF	1.7
\.


--
-- Data for Name: polygongeofence; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.polygongeofence (id, geofenceid, latitude, longitude) FROM stdin;
6	8	40.7128	-74.006
7	8	40.759	-73.9845
8	8	40.7711	-73.9677
9	11	40.7587	-73.9753
10	11	40.7625	-73.9712
11	14	40.7587	-73.9753
12	14	40.7625	-73.9712
13	17	40.7587	-73.9753
14	17	40.7625	-73.9712
\.


--
-- Data for Name: rectanglegeofence; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.rectanglegeofence (id, geofenceid, north, east, west, south) FROM stdin;
3	7	40.7128	-74.006	40.7128	-74.006
4	10	40.759	-73.9845	40.759	-73.9845
5	13	40.7711	-73.9677	40.7711	-73.9677
6	16	40.7587	-73.9753	40.7587	-73.9753
\.


--
-- Data for Name: routehistory; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.routehistory (routehistoryid, vehicleid, vehicledirection, status, vehiclespeed, address, latitude, longitude, recordtime) FROM stdin;
6	1	90	1	60 mph	123 Main St	40.7128	-74.006	1614556800
7	2	180	0	50 mph	456 Broadway	40.759	-73.9845	1614643200
8	3	270	1	70 mph	789 Park Ave	40.7711	-73.9677	1614729600
9	4	360	0	80 mph	321 Madison Ave	40.7587	-73.9753	1614816000
10	5	45	1	90 mph	654 Lexington Ave	40.7625	-73.9712	1614902400
11	5	1	A	100	Riyadh	24.7136	46.6753	1612556800
\.


--
-- Data for Name: vehicles; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.vehicles (vehicleid, vehiclenumber, vehicletype) FROM stdin;
1	1234567890	Truck
2	2345678901	Car
3	3456789012	Bus
4	4567890123	Van
5	5678901234	Motorcycle
\.


--
-- Data for Name: vehiclesinformations; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.vehiclesinformations (id, vehicleid, driverid, vehiclemake, vehiclemodel, purchasedate) FROM stdin;
1	1	1	Toyota	Corolla	1614556800
2	2	2	Honda	Civic	1614643200
4	4	4	Chevrolet	Camaro	1614816000
3	3	4	Ford	Mustang	1614729600
\.


--
-- Name: circlegeofence_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.circlegeofence_id_seq', 12, true);


--
-- Name: drivers_driverid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.drivers_driverid_seq', 7, true);


--
-- Name: geofences_geofenceid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.geofences_geofenceid_seq', 17, true);


--
-- Name: polygongeofence_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.polygongeofence_id_seq', 14, true);


--
-- Name: rectanglegeofence_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.rectanglegeofence_id_seq', 6, true);


--
-- Name: routehistory_routehistoryid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.routehistory_routehistoryid_seq', 11, true);


--
-- Name: vehicles_vehicleid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.vehicles_vehicleid_seq', 6, true);


--
-- Name: vehiclesinformations_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.vehiclesinformations_id_seq', 6, true);


--
-- Name: vehiclesinformations UQ_VehicleId; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.vehiclesinformations
    ADD CONSTRAINT "UQ_VehicleId" UNIQUE (vehicleid);


--
-- Name: circlegeofence circlegeofence_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.circlegeofence
    ADD CONSTRAINT circlegeofence_pkey PRIMARY KEY (id);


--
-- Name: driver drivers_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.driver
    ADD CONSTRAINT drivers_pkey PRIMARY KEY (driverid);


--
-- Name: geofences geofences_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.geofences
    ADD CONSTRAINT geofences_pkey PRIMARY KEY (geofenceid);


--
-- Name: polygongeofence polygongeofence_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.polygongeofence
    ADD CONSTRAINT polygongeofence_pkey PRIMARY KEY (id);


--
-- Name: rectanglegeofence rectanglegeofence_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.rectanglegeofence
    ADD CONSTRAINT rectanglegeofence_pkey PRIMARY KEY (id);


--
-- Name: routehistory routehistory_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.routehistory
    ADD CONSTRAINT routehistory_pkey PRIMARY KEY (routehistoryid);


--
-- Name: vehicles vehicles_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.vehicles
    ADD CONSTRAINT vehicles_pkey PRIMARY KEY (vehicleid);


--
-- Name: vehiclesinformations vehiclesinformations_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.vehiclesinformations
    ADD CONSTRAINT vehiclesinformations_pkey PRIMARY KEY (id);


--
-- Name: circlegeofence circlegeofence_geofenceid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.circlegeofence
    ADD CONSTRAINT circlegeofence_geofenceid_fkey FOREIGN KEY (geofenceid) REFERENCES public.geofences(geofenceid);


--
-- Name: polygongeofence polygongeofence_geofenceid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.polygongeofence
    ADD CONSTRAINT polygongeofence_geofenceid_fkey FOREIGN KEY (geofenceid) REFERENCES public.geofences(geofenceid);


--
-- Name: rectanglegeofence rectanglegeofence_geofenceid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.rectanglegeofence
    ADD CONSTRAINT rectanglegeofence_geofenceid_fkey FOREIGN KEY (geofenceid) REFERENCES public.geofences(geofenceid);


--
-- Name: routehistory routehistory_vehicleid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.routehistory
    ADD CONSTRAINT routehistory_vehicleid_fkey FOREIGN KEY (vehicleid) REFERENCES public.vehicles(vehicleid);


--
-- Name: vehiclesinformations vehiclesinformations_driverid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.vehiclesinformations
    ADD CONSTRAINT vehiclesinformations_driverid_fkey FOREIGN KEY (driverid) REFERENCES public.driver(driverid);


--
-- Name: vehiclesinformations vehiclesinformations_vehicleid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.vehiclesinformations
    ADD CONSTRAINT vehiclesinformations_vehicleid_fkey FOREIGN KEY (vehicleid) REFERENCES public.vehicles(vehicleid);


--
-- PostgreSQL database dump complete
--

