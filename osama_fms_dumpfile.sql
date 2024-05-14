--
-- PostgreSQL database dump
--

-- Dumped from database version 16.2
-- Dumped by pg_dump version 16.2

-- Started on 2024-05-14 15:57:33

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
-- TOC entry 215 (class 1259 OID 16399)
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
-- TOC entry 216 (class 1259 OID 16402)
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
-- TOC entry 4919 (class 0 OID 0)
-- Dependencies: 216
-- Name: circlegeofence_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.circlegeofence_id_seq OWNED BY public.circlegeofence.id;


--
-- TOC entry 217 (class 1259 OID 16403)
-- Name: driver; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.driver (
    driverid bigint NOT NULL,
    drivername character varying,
    phonenumber bigint
);


ALTER TABLE public.driver OWNER TO postgres;

--
-- TOC entry 218 (class 1259 OID 16408)
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
-- TOC entry 4920 (class 0 OID 0)
-- Dependencies: 218
-- Name: drivers_driverid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.drivers_driverid_seq OWNED BY public.driver.driverid;


--
-- TOC entry 219 (class 1259 OID 16409)
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
-- TOC entry 220 (class 1259 OID 16414)
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
-- TOC entry 4921 (class 0 OID 0)
-- Dependencies: 220
-- Name: geofences_geofenceid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.geofences_geofenceid_seq OWNED BY public.geofences.geofenceid;


--
-- TOC entry 221 (class 1259 OID 16415)
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
-- TOC entry 222 (class 1259 OID 16418)
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
-- TOC entry 4922 (class 0 OID 0)
-- Dependencies: 222
-- Name: polygongeofence_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.polygongeofence_id_seq OWNED BY public.polygongeofence.id;


--
-- TOC entry 223 (class 1259 OID 16419)
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
-- TOC entry 224 (class 1259 OID 16422)
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
-- TOC entry 4923 (class 0 OID 0)
-- Dependencies: 224
-- Name: rectanglegeofence_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.rectanglegeofence_id_seq OWNED BY public.rectanglegeofence.id;


--
-- TOC entry 225 (class 1259 OID 16423)
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
-- TOC entry 226 (class 1259 OID 16428)
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
-- TOC entry 4924 (class 0 OID 0)
-- Dependencies: 226
-- Name: routehistory_routehistoryid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.routehistory_routehistoryid_seq OWNED BY public.routehistory.routehistoryid;


--
-- TOC entry 227 (class 1259 OID 16429)
-- Name: vehicles; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.vehicles (
    vehicleid bigint NOT NULL,
    vehiclenumber bigint,
    vehicletype character varying
);


ALTER TABLE public.vehicles OWNER TO postgres;

--
-- TOC entry 228 (class 1259 OID 16434)
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
-- TOC entry 4925 (class 0 OID 0)
-- Dependencies: 228
-- Name: vehicles_vehicleid_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.vehicles_vehicleid_seq OWNED BY public.vehicles.vehicleid;


--
-- TOC entry 229 (class 1259 OID 16435)
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
-- TOC entry 230 (class 1259 OID 16440)
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
-- TOC entry 4926 (class 0 OID 0)
-- Dependencies: 230
-- Name: vehiclesinformations_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.vehiclesinformations_id_seq OWNED BY public.vehiclesinformations.id;


--
-- TOC entry 4723 (class 2604 OID 16441)
-- Name: circlegeofence id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.circlegeofence ALTER COLUMN id SET DEFAULT nextval('public.circlegeofence_id_seq'::regclass);


--
-- TOC entry 4724 (class 2604 OID 16442)
-- Name: driver driverid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.driver ALTER COLUMN driverid SET DEFAULT nextval('public.drivers_driverid_seq'::regclass);


--
-- TOC entry 4725 (class 2604 OID 16443)
-- Name: geofences geofenceid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.geofences ALTER COLUMN geofenceid SET DEFAULT nextval('public.geofences_geofenceid_seq'::regclass);


--
-- TOC entry 4726 (class 2604 OID 16444)
-- Name: polygongeofence id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.polygongeofence ALTER COLUMN id SET DEFAULT nextval('public.polygongeofence_id_seq'::regclass);


--
-- TOC entry 4727 (class 2604 OID 16445)
-- Name: rectanglegeofence id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.rectanglegeofence ALTER COLUMN id SET DEFAULT nextval('public.rectanglegeofence_id_seq'::regclass);


--
-- TOC entry 4728 (class 2604 OID 16446)
-- Name: routehistory routehistoryid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.routehistory ALTER COLUMN routehistoryid SET DEFAULT nextval('public.routehistory_routehistoryid_seq'::regclass);


--
-- TOC entry 4729 (class 2604 OID 16447)
-- Name: vehicles vehicleid; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.vehicles ALTER COLUMN vehicleid SET DEFAULT nextval('public.vehicles_vehicleid_seq'::regclass);


--
-- TOC entry 4730 (class 2604 OID 16448)
-- Name: vehiclesinformations id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.vehiclesinformations ALTER COLUMN id SET DEFAULT nextval('public.vehiclesinformations_id_seq'::regclass);


--
-- TOC entry 4898 (class 0 OID 16399)
-- Dependencies: 215
-- Data for Name: circlegeofence; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.circlegeofence (id, geofenceid, radius, latitude, longitude) FROM stdin;
9	6	1000	40.7128	-74.006
10	9	2000	40.759	-73.9845
11	12	3000	40.7711	-73.9677
12	15	4000	40.7587	-73.9753
\.


--
-- TOC entry 4900 (class 0 OID 16403)
-- Dependencies: 217
-- Data for Name: driver; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.driver (driverid, drivername, phonenumber) FROM stdin;
3	Bob Johnson	3456789012
4	Alice Williams	4567890123
5	Charlie Brown	5678901234
32	David Smith	3456789012
33	Emily Davis	4567890123
34	Frank Miller	5678901234
35	Grace Lee	6789012345
36	Henry Wilson	7890123456
37	Isabella Thompson	8901234567
38	Jack Anderson	9012345678
39	osama sholi	59785
40	Robert Smith	9876543210
41	osama	98756
2	Jane Smith	23456789015
44	osama	123513212
46	adw	4123
\.


--
-- TOC entry 4902 (class 0 OID 16409)
-- Dependencies: 219
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
-- TOC entry 4904 (class 0 OID 16415)
-- Dependencies: 221
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
-- TOC entry 4906 (class 0 OID 16419)
-- Dependencies: 223
-- Data for Name: rectanglegeofence; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.rectanglegeofence (id, geofenceid, north, east, west, south) FROM stdin;
3	7	40.7128	-74.006	40.7128	-74.006
4	10	40.759	-73.9845	40.759	-73.9845
5	13	40.7711	-73.9677	40.7711	-73.9677
6	16	40.7587	-73.9753	40.7587	-73.9753
\.


--
-- TOC entry 4908 (class 0 OID 16423)
-- Dependencies: 225
-- Data for Name: routehistory; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.routehistory (routehistoryid, vehicleid, vehicledirection, status, vehiclespeed, address, latitude, longitude, recordtime) FROM stdin;
27	11	130	1	200	street	120	43	1715691141
28	11	32	0	0	street1	12	-20	1715691160
29	8	23	1	42	city	12	-90	1715691213
30	8	12	0	123	yes	12.5	327	1715691233
31	8	31	1	25	street4	12	32	1715691266
\.


--
-- TOC entry 4910 (class 0 OID 16429)
-- Dependencies: 227
-- Data for Name: vehicles; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.vehicles (vehicleid, vehiclenumber, vehicletype) FROM stdin;
11	12345	test
8	125348	busa
13	1236543	qwe
14	1231426	Engine Bike
15	3245	Van
\.


--
-- TOC entry 4912 (class 0 OID 16435)
-- Dependencies: 229
-- Data for Name: vehiclesinformations; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.vehiclesinformations (id, vehicleid, driverid, vehiclemake, vehiclemodel, purchasedate) FROM stdin;
20	8	40	awd	asdw	1718367872
21	11	3	make	model	1715691126
\.


--
-- TOC entry 4927 (class 0 OID 0)
-- Dependencies: 216
-- Name: circlegeofence_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.circlegeofence_id_seq', 12, true);


--
-- TOC entry 4928 (class 0 OID 0)
-- Dependencies: 218
-- Name: drivers_driverid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.drivers_driverid_seq', 46, true);


--
-- TOC entry 4929 (class 0 OID 0)
-- Dependencies: 220
-- Name: geofences_geofenceid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.geofences_geofenceid_seq', 17, true);


--
-- TOC entry 4930 (class 0 OID 0)
-- Dependencies: 222
-- Name: polygongeofence_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.polygongeofence_id_seq', 14, true);


--
-- TOC entry 4931 (class 0 OID 0)
-- Dependencies: 224
-- Name: rectanglegeofence_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.rectanglegeofence_id_seq', 6, true);


--
-- TOC entry 4932 (class 0 OID 0)
-- Dependencies: 226
-- Name: routehistory_routehistoryid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.routehistory_routehistoryid_seq', 31, true);


--
-- TOC entry 4933 (class 0 OID 0)
-- Dependencies: 228
-- Name: vehicles_vehicleid_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.vehicles_vehicleid_seq', 18, true);


--
-- TOC entry 4934 (class 0 OID 0)
-- Dependencies: 230
-- Name: vehiclesinformations_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.vehiclesinformations_id_seq', 21, true);


--
-- TOC entry 4746 (class 2606 OID 16450)
-- Name: vehiclesinformations UQ_VehicleId; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.vehiclesinformations
    ADD CONSTRAINT "UQ_VehicleId" UNIQUE (vehicleid);


--
-- TOC entry 4732 (class 2606 OID 16452)
-- Name: circlegeofence circlegeofence_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.circlegeofence
    ADD CONSTRAINT circlegeofence_pkey PRIMARY KEY (id);


--
-- TOC entry 4734 (class 2606 OID 16454)
-- Name: driver drivers_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.driver
    ADD CONSTRAINT drivers_pkey PRIMARY KEY (driverid);


--
-- TOC entry 4736 (class 2606 OID 16456)
-- Name: geofences geofences_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.geofences
    ADD CONSTRAINT geofences_pkey PRIMARY KEY (geofenceid);


--
-- TOC entry 4738 (class 2606 OID 16458)
-- Name: polygongeofence polygongeofence_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.polygongeofence
    ADD CONSTRAINT polygongeofence_pkey PRIMARY KEY (id);


--
-- TOC entry 4740 (class 2606 OID 16460)
-- Name: rectanglegeofence rectanglegeofence_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.rectanglegeofence
    ADD CONSTRAINT rectanglegeofence_pkey PRIMARY KEY (id);


--
-- TOC entry 4742 (class 2606 OID 16462)
-- Name: routehistory routehistory_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.routehistory
    ADD CONSTRAINT routehistory_pkey PRIMARY KEY (routehistoryid);


--
-- TOC entry 4744 (class 2606 OID 16464)
-- Name: vehicles vehicles_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.vehicles
    ADD CONSTRAINT vehicles_pkey PRIMARY KEY (vehicleid);


--
-- TOC entry 4748 (class 2606 OID 16466)
-- Name: vehiclesinformations vehiclesinformations_pkey; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.vehiclesinformations
    ADD CONSTRAINT vehiclesinformations_pkey PRIMARY KEY (id);


--
-- TOC entry 4749 (class 2606 OID 16518)
-- Name: circlegeofence circlegeofence_geofenceid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.circlegeofence
    ADD CONSTRAINT circlegeofence_geofenceid_fkey FOREIGN KEY (geofenceid) REFERENCES public.geofences(geofenceid) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- TOC entry 4750 (class 2606 OID 16523)
-- Name: polygongeofence polygongeofence_geofenceid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.polygongeofence
    ADD CONSTRAINT polygongeofence_geofenceid_fkey FOREIGN KEY (geofenceid) REFERENCES public.geofences(geofenceid) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- TOC entry 4751 (class 2606 OID 16528)
-- Name: rectanglegeofence rectanglegeofence_geofenceid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.rectanglegeofence
    ADD CONSTRAINT rectanglegeofence_geofenceid_fkey FOREIGN KEY (geofenceid) REFERENCES public.geofences(geofenceid) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- TOC entry 4752 (class 2606 OID 16513)
-- Name: routehistory routehistory_vehicleid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.routehistory
    ADD CONSTRAINT routehistory_vehicleid_fkey FOREIGN KEY (vehicleid) REFERENCES public.vehicles(vehicleid) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


--
-- TOC entry 4753 (class 2606 OID 16503)
-- Name: vehiclesinformations vehiclesinformations_driverid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.vehiclesinformations
    ADD CONSTRAINT vehiclesinformations_driverid_fkey FOREIGN KEY (driverid) REFERENCES public.driver(driverid) ON UPDATE SET NULL ON DELETE SET NULL NOT VALID;


--
-- TOC entry 4754 (class 2606 OID 16508)
-- Name: vehiclesinformations vehiclesinformations_vehicleid_fkey; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.vehiclesinformations
    ADD CONSTRAINT vehiclesinformations_vehicleid_fkey FOREIGN KEY (vehicleid) REFERENCES public.vehicles(vehicleid) ON UPDATE CASCADE ON DELETE CASCADE NOT VALID;


-- Completed on 2024-05-14 15:57:33

--
-- PostgreSQL database dump complete
--
