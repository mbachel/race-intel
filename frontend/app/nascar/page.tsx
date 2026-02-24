"use client";

import Link from "next/link";
import { useMemo, useState } from "react";
import { VscTriangleUp, VscTriangleDown } from "react-icons/vsc";
import Modal from "../components/Modal";

export default function NascarLivePage() {
  const [positionsOpen, setPositionsOpen] = useState(false);
  const [search, setSearch] = useState("");
  const recentRaces = [
    {
      name: "Daytona 500",
      location: "Daytona",
      date: "Feb 16, 2026",
      slug: "daytona-500-2026",
    },
    {
      name: "Atlanta 400",
      location: "Atlanta",
      date: "Feb 23, 2026",
      slug: "atlanta-400-2026",
    },
    {
      name: "Las Vegas 400",
      location: "Las Vegas",
      date: "Mar 02, 2026",
      slug: "las-vegas-400-2026",
    },
    {
      name: "Phoenix 500",
      location: "Phoenix",
      date: "Mar 09, 2026",
      slug: "phoenix-500-2026",
    },
    {
      name: "Auto Club 400",
      location: "Fontana",
      date: "Mar 23, 2026",
      slug: "auto-club-400-2026",
    },
  ];

  const filteredRaces = useMemo(() => {
    const query = search.trim().toLowerCase();
    if (!query) {
      return recentRaces;
    }

    return recentRaces.filter((race) =>
      `${race.name} ${race.location} ${race.date}`
        .toLowerCase()
        .includes(query)
    );
  }, [recentRaces, search]);

  const winPredictions = [
    { driver: "Larson", pct: 28 },
    { driver: "Hamlin", pct: 22 },
    { driver: "Elliott", pct: 15 },
    { driver: "Wallace", pct: 12 },
    { driver: "Blaney", pct: 9 },
    { driver: "Byron", pct: 6 },
    { driver: "Chastain", pct: 4 },
    { driver: "Field", pct: 4 },
  ];

  const fullStandings = [
    {
      pos: 1,
      driver: "Kyle Larson",
      car: "5",
      gap: "+0.000s",
      lastLap: "46.820s",
      avgSpeed: "187.4 mph",
      passRate: "12 / 4 / 8",
      start: 3,
      diff: 2,
      bestLap: "46.71s",
    },
    {
      pos: 2,
      driver: "Denny Hamlin",
      car: "11",
      gap: "+0.600s",
      lastLap: "46.900s",
      avgSpeed: "186.8 mph",
      passRate: "10 / 5 / 6",
      start: 5,
      diff: 3,
      bestLap: "46.78s",
    },
    {
      pos: 3,
      driver: "Chase Elliott",
      car: "9",
      gap: "+1.200s",
      lastLap: "46.980s",
      avgSpeed: "186.3 mph",
      passRate: "9 / 6 / 5",
      start: 1,
      diff: -2,
      bestLap: "46.85s",
    },
    {
      pos: 4,
      driver: "Bubba Wallace",
      car: "23",
      gap: "+1.900s",
      lastLap: "47.050s",
      avgSpeed: "185.9 mph",
      passRate: "14 / 3 / 10",
      start: 10,
      diff: 6,
      bestLap: "46.92s",
    },
    {
      pos: 5,
      driver: "Ryan Blaney",
      car: "12",
      gap: "+2.400s",
      lastLap: "47.080s",
      avgSpeed: "185.6 mph",
      passRate: "8 / 7 / 4",
      start: 7,
      diff: 2,
      bestLap: "46.95s",
    },
    {
      pos: 6,
      driver: "William Byron",
      car: "24",
      gap: "+3.100s",
      lastLap: "47.120s",
      avgSpeed: "185.2 mph",
      passRate: "7 / 8 / 3",
      start: 4,
      diff: -2,
      bestLap: "47.01s",
    },
    {
      pos: 7,
      driver: "Ross Chastain",
      car: "1",
      gap: "+3.800s",
      lastLap: "47.160s",
      avgSpeed: "184.9 mph",
      passRate: "6 / 9 / 3",
      start: 8,
      diff: 1,
      bestLap: "47.05s",
    },
    {
      pos: 8,
      driver: "Tyler Reddick",
      car: "45",
      gap: "+4.600s",
      lastLap: "47.200s",
      avgSpeed: "184.5 mph",
      passRate: "5 / 10 / 2",
      start: 6,
      diff: -2,
      bestLap: "47.10s",
    },
    {
      pos: 9,
      driver: "Joey Logano",
      car: "22",
      gap: "+5.200s",
      lastLap: "47.230s",
      avgSpeed: "184.2 mph",
      passRate: "4 / 11 / 2",
      start: 2,
      diff: -7,
      bestLap: "47.14s",
    },
    {
      pos: 10,
      driver: "Brad Keselowski",
      car: "6",
      gap: "+5.900s",
      lastLap: "47.260s",
      avgSpeed: "183.9 mph",
      passRate: "3 / 12 / 1",
      start: 9,
      diff: -1,
      bestLap: "47.18s",
    },
  ];

  // Preview uses the same shape as fullStandings so the table columns match
  const liveStandings = fullStandings.slice(0, 8);

  const positionAnalytics = [
    { label: "Leader Average Speed", value: "187.4 mph", note: "Larson avg speed this run" },
    { label: "Biggest Movers", value: "+6 spots", note: "Wallace P10 → P4 last 20 laps" },
    { label: "Average Gap", value: "1.8s", note: "Mean delta across top 10" },
  ];

  const isLiveRaceAvailable = true;

  return (
    <div className="series-layout">
      <aside className="series-sidebar">
        <div className="panel p-5">
          <div className="flex items-center justify-between">
            <h2 className="section-title">Recent races</h2>
            <span className="chip">NASCAR</span>
          </div>
          <label className="search-label" htmlFor="nascar-race-search">
            Search races
          </label>
          <input
            id="nascar-race-search"
            className="search-input"
            type="search"
            placeholder="Search by race, city, date"
            value={search}
            onChange={(event) => setSearch(event.target.value)}
          />
          <div className="race-list">
            {filteredRaces.map((race) => (
              <Link
                key={race.slug}
                className="race-link"
                href={`/nascar/${race.slug}`}
              >
                <span className="race-name">{race.name}</span>
                <span className="race-meta">{race.location}</span>
                <span className="race-meta">{race.date}</span>
              </Link>
            ))}
            {filteredRaces.length === 0 && (
              <span className="muted">No races match your search.</span>
            )}
          </div>
        </div>
      </aside>

      <div className="series-main">
        {isLiveRaceAvailable ? (
          <>
            <section className="panel p-8 md:p-12 fade-up">
              <div className="flex flex-col gap-4">
                <span className="chip">Live now</span>
                <h1 className="hero-title">Daytona 500 performance command</h1>
                <p className="hero-subtitle muted">
                  Drafting, fuel, and overtaking analytics with live position tracking.
                  AI insights are placeholders until the feed is integrated.
                </p>
                <div className="flex flex-wrap gap-3">
                  <span className="panel-soft px-4 py-2 text-sm">
                    Stage: 1 of 3
                  </span>
                  <span className="panel-soft px-4 py-2 text-sm">Lap: 34 / 200</span>
                  <span className="panel-soft px-4 py-2 text-sm">
                    Caution count: 0
                  </span>
                </div>
              </div>
            </section>

            <section className="grid gap-6 lg:grid-cols-[1.6fr_1fr] fade-up delay-1">
              <div className="panel p-6 md:p-8">
                <div className="flex items-center justify-between">
                  <h2 className="section-title">
                    <button
                      type="button"
                      className="section-title-button"
                      onClick={() => setPositionsOpen(true)}
                    >
                      Live positions
                    </button>
                  </h2>
                  <span className="chip">Green flag</span>
                </div>
                <div className="mt-6 overflow-x-auto">
                  <table className="data-table">
                    <thead>
                      <tr>
                        <th>Pos</th>
                        <th>Driver</th>
                        <th>Car</th>
                        <th>Gap</th>
                        <th>Last Lap</th>
                        <th>Avg Speed</th>
                        <th>+/−</th>
                        <th>Best Lap</th>
                      </tr>
                    </thead>
                    <tbody>
                      {liveStandings.map((row) => (
                        <tr key={row.pos}>
                          <td>{row.pos}</td>
                          <td>{row.driver}</td>
                          <td>#{row.car}</td>
                          <td>{row.gap}</td>
                          <td>{row.lastLap}</td>
                          <td>{row.avgSpeed}</td>
                          <td className={row.diff > 0 ? "text-green-500" : row.diff < 0 ? "text-red-500" : ""}>
                            <span className="inline-flex items-center gap-0.5">
                              {row.diff > 0 && <VscTriangleUp className="text-green-500" />}
                              {row.diff < 0 && <VscTriangleDown className="text-red-500" />}
                              {Math.abs(row.diff)}
                            </span>
                          </td>
                          <td>{row.bestLap}</td>
                        </tr>
                      ))}
                    </tbody>
                  </table>
                </div>
              </div>

              <div className="panel p-6 md:p-8 flex flex-col items-center">
                <h2 className="section-title self-start">Track Map</h2>
                <div className="mt-6 flex items-center justify-center w-full" style={{ minHeight: 520 }}>
                  {/* Daytona International Speedway SVG – rotated 90° for vertical display */}
                  {/* eslint-disable-next-line @next/next/no-img-element */}
                  <img
                    src="/tracks/light/Daytona_International_Speedway edited.svg"
                    alt="Daytona International Speedway track map"
                    style={{
                      transform: "rotate(90deg)",
                      maxHeight: 500,
                      width: "auto",
                      filter: "contrast(1.25) brightness(0.95)",
                    }}
                  />
                </div>
              </div>
            </section>

            <section className="panel p-6 md:p-8 fade-up delay-2">
              <div className="flex items-center justify-between">
                <h2 className="section-title">Win predictor</h2>
                <span className="muted text-sm">AI assisted metrics</span>
              </div>
              <div className="mt-6 space-y-3">
                {winPredictions.map((d) => (
                  <div key={d.driver} className="flex items-center gap-3">
                    <span className="w-24 text-sm font-medium truncate">{d.driver}</span>
                    <div className="flex-1 h-6 rounded-md overflow-hidden" style={{ background: "var(--panel-soft-bg, rgba(128,128,128,0.12))" }}>
                      <div
                        className="h-full rounded-md"
                        style={{
                          width: `${d.pct}%`,
                          background: "linear-gradient(90deg, var(--accent, #3b82f6), var(--accent-hover, #60a5fa))",
                          transition: "width 0.6s ease",
                        }}
                      />
                    </div>
                    <span className="w-12 text-sm font-semibold text-right">{d.pct}%</span>
                  </div>
                ))}
              </div>
            </section>
          </>
        ) : (
          <section className="panel p-8 md:p-12 fade-up">
            <div className="flex flex-col gap-4 text-center">
              <span className="chip mx-auto">Live status</span>
              <h1 className="hero-title">No current live race</h1>
              <p className="hero-subtitle muted">Check again later.</p>
            </div>
          </section>
        )}
      </div>

      {isLiveRaceAvailable && (
        <>
      <Modal
        isOpen={positionsOpen}
        title="NASCAR live positions + analytics"
        onClose={() => setPositionsOpen(false)}
      >
        <div className="grid gap-4 md:grid-cols-3">
          {positionAnalytics.map((item) => (
            <div key={item.label} className="panel-soft p-4">
              <p className="text-sm uppercase tracking-widest muted">
                {item.label}
              </p>
              <p className="mt-2 text-2xl font-semibold">{item.value}</p>
              <p className="mt-2 text-sm muted">{item.note}</p>
            </div>
          ))}
        </div>
        <div className="panel-soft p-4 overflow-x-auto">
          <table className="data-table">
            <thead>
              <tr>
                <th>Pos</th>
                <th>Driver</th>
                <th>Car</th>
                <th>Gap</th>
                <th>Last Lap</th>
                <th>Avg Speed</th>
                <th title="Passes Made / Times Passed / Quality Passes">Pass Rate</th>
                <th>Start</th>
                <th>+/−</th>
                <th>Best Lap</th>
              </tr>
            </thead>
            <tbody>
              {fullStandings.map((row) => (
                <tr key={row.pos}>
                  <td>{row.pos}</td>
                  <td>{row.driver}</td>
                  <td>#{row.car}</td>
                  <td>{row.gap}</td>
                  <td>{row.lastLap}</td>
                  <td>{row.avgSpeed}</td>
                  <td>{row.passRate}</td>
                  <td>{row.start}</td>
                  <td className={row.diff > 0 ? "text-green-500" : row.diff < 0 ? "text-red-500" : ""}>
                    <span className="inline-flex items-center gap-0.5">
                      {row.diff > 0 && <VscTriangleUp className="text-green-500" />}
                      {row.diff < 0 && <VscTriangleDown className="text-red-500" />}
                      {Math.abs(row.diff)}
                    </span>
                  </td>
                  <td>{row.bestLap}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </Modal>


        </>
      )}
    </div>
  );
}
