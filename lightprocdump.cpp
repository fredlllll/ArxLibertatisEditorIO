//dumped from arx source, added comments

//applies light to an eeriepoly
void EERIE_LIGHT_Apply(EERIEPOLY * ep, EERIEPOLY * father)
{
	if (ep->type & POLY_IGNORE)  return;

	long i;
	//eeriepoly rgb values for each vertex
	float epr[4];
	float epg[4];
	float epb[4];

	epr[3] = epr[2] = epr[1] = epr[0] = 0; 
	epg[3] = epg[2] = epg[1] = epg[0] = 0; 
	epb[3] = epb[2] = epb[1] = epb[0] = 0; 

	for (i = 0; i < MAX_LIGHTS; i++)
	{
		EERIE_LIGHT * el = GLight[i];

		if ((el) && (el->treat) && (el->exist) && (el->status) 
		        && !(el->extras & EXTRAS_SEMIDYNAMIC))
		{
			if (Distance3D(el->pos.x, el->pos.y, el->pos.z,
			               ep->center.x, ep->center.y, ep->center.z) < el->fallend + 100.f)
				EERIE_LIGHT_Make(ep, epr, epg, epb, el, father);
		}
	}

	for (i = 0; i < MAX_ACTIONS; i++)
	{
		if ((actions[i].exist) && ((actions[i].type == ACT_FIRE2) || (actions[i].type == ACT_FIRE)))
		{
			if (Distance3D(actions[i].light.pos.x, actions[i].light.pos.y, actions[i].light.pos.z,
			               ep->center.x, ep->center.y, ep->center.z) < actions[i].light.fallend + 100.f)
				EERIE_LIGHT_Make(ep, epr, epg, epb, &actions[i].light, father);
		}
	}

	long nbvert;

	if (ep->type & POLY_QUAD) nbvert = 4;
	else nbvert = 3;

	for (i = 0; i < nbvert; i++)
	{
		if (epr[i] > 1.f) epr[i] = 1.f;
		else if (epr[i] < ACTIVEBKG->ambient.r) epr[i] = ACTIVEBKG->ambient.r;

		if (epg[i] > 1.f) epg[i] = 1.f;
		else if (epg[i] < ACTIVEBKG->ambient.g) epg[i] = ACTIVEBKG->ambient.g;

		if (epb[i] > 1.f) epb[i] = 1.f;
		else if (epb[i] < ACTIVEBKG->ambient.b) epb[i] = ACTIVEBKG->ambient.b;

		ep->v[i].color = EERIERGB(epr[i], epg[i], epb[i]);
	}
}