"use client";

import { useState } from "react";
import Modal from "../components/Modal";

export default function NascarLivePage() {
  const [positionsOpen, setPositionsOpen] = useState(false);
  const [burnBarOpen, setBurnBarOpen] = useState(false);
  const [insightsOpen, setInsightsOpen] = useState(false);
  const liveStandings = [
    {
      pos: "1",
      driver: "Larson",
      car: "5",
      gap: "+0.0",
      fuel: "89%",
      tires: "4 fresh",
    },
    {
      pos: "2",
      driver: "Hamlin",
      car: "11",
      gap: "+0.6",
      fuel: "87%",
      tires: "4 fresh",
    },
    {
      pos: "3",
      driver: "Elliott",
      car: "9",
      gap: "+1.2",
      fuel: "85%",
      tires: "4 fresh",
    },
    {
      pos: "4",
      driver: "Wallace",
      car: "23",
      gap: "+1.9",
      fuel: "83%",
      tires: "2 fresh",
    },
    {
      pos: "5",
      driver: "Blaney",
      car: "12",
      gap: "+2.4",
      fuel: "82%",
      tires: "4 fresh",
    },
  ];

  const burnBar = [
    {
      title: "Fuel burn",
      value: "104%",
      detail: "Lead pack running above target burn rate.",
    },
    {
      title: "Draft efficiency",
      value: "76%",
      detail: "Top 10 spending 12s per lap in clean air.",
    },
    {
      title: "Passer rating",
      value: "8.4",
      detail: "Overtakes completed within 2.1s on average.",
    },
  ];

  const insights = [
    {
      title: "Time in range",
      detail: "P6-P10 spent 18s in passing range without a clear pass.",
    },
    {
      title: "Clean vs dirty air",
      detail: "Dirty air cost is 0.42s in turns 1-2.",
    },
    {
      title: "Loop data rating",
      detail: "Larson 142.3, Hamlin 139.8, Elliott 138.1.",
    },
  ];

  const fullStandings = [
    {
      pos: "1",
      driver: "Larson",
      car: "5",
      gap: "+0.0",
      fuel: "89%",
      tires: "4 fresh",
      lastLap: "46.82",
      avgRun: "47.10",
      draft: "14s",
      passRate: "82%",
    },
    {
      pos: "2",
      driver: "Hamlin",
      car: "11",
      gap: "+0.6",
      fuel: "87%",
      tires: "4 fresh",
      lastLap: "46.90",
      avgRun: "47.18",
      draft: "16s",
      passRate: "79%",
    },
    {
      pos: "3",
      driver: "Elliott",
      car: "9",
      gap: "+1.2",
      fuel: "85%",
      tires: "4 fresh",
      lastLap: "46.98",
      avgRun: "47.22",
      draft: "15s",
      passRate: "76%",
    },
    {
      pos: "4",
      driver: "Wallace",
      car: "23",
      gap: "+1.9",
      fuel: "83%",
      tires: "2 fresh",
      lastLap: "47.05",
      avgRun: "47.30",
      draft: "18s",
      passRate: "72%",
    },
    {
      pos: "5",
      driver: "Blaney",
      car: "12",
      gap: "+2.4",
      fuel: "82%",
      tires: "4 fresh",
      lastLap: "47.08",
      avgRun: "47.32",
      draft: "13s",
      passRate: "74%",
    },
    {
      pos: "6",
      driver: "Byron",
      car: "24",
      gap: "+3.1",
      fuel: "81%",
      tires: "4 fresh",
      lastLap: "47.12",
      avgRun: "47.40",
      draft: "19s",
      passRate: "70%",
    },
    {
      pos: "7",
      driver: "Chastain",
      car: "1",
      gap: "+3.8",
      fuel: "80%",
      tires: "4 fresh",
      lastLap: "47.16",
      avgRun: "47.41",
      draft: "17s",
      passRate: "68%",
    },
    {
      pos: "8",
      driver: "Reddick",
      car: "45",
      gap: "+4.6",
      fuel: "79%",
      tires: "2 fresh",
      lastLap: "47.20",
      avgRun: "47.45",
      draft: "20s",
      passRate: "66%",
    },
    {
      pos: "9",
      driver: "Logano",
      car: "22",
      gap: "+5.2",
      fuel: "78%",
      tires: "4 fresh",
      lastLap: "47.23",
      avgRun: "47.51",
      draft: "16s",
      passRate: "64%",
    },
    {
      pos: "10",
      driver: "Keselowski",
      car: "6",
      gap: "+5.9",
      fuel: "77%",
      tires: "4 fresh",
      lastLap: "47.26",
      avgRun: "47.55",
      draft: "15s",
      passRate: "63%",
    },
  ];

  const positionAnalytics = [
    { label: "Leader avg lap", value: "47.10", note: "Larson last 10 laps" },
    { label: "Fuel window", value: "34 laps", note: "Based on burn rate" },
    { label: "Passer range", value: "2.1s", note: "Median time to clear" },
  ];

  const burnBarDetails = [
    {
      label: "Burn target",
      value: "98%",
      note: "Need lift/coast in turns 3-4.",
    },
    {
      label: "Draft save",
      value: "+2.4L",
      note: "Projected if running P6-P10.",
    },
    {
      label: "Pit delta",
      value: "-1.1s",
      note: "Green flag pit time vs staying out.",
    },
  ];

  const burnTable = [
    { driver: "Larson", burn: "104%", trend: "Rising", risk: "High" },
    { driver: "Hamlin", burn: "101%", trend: "Stable", risk: "Medium" },
    { driver: "Elliott", burn: "99%", trend: "Falling", risk: "Low" },
    { driver: "Wallace", burn: "106%", trend: "Rising", risk: "High" },
  ];

  const insightsTable = [
    { driver: "Larson", rating: "142.3", cleanAir: "+0.24", timeInRange: "6.8s" },
    { driver: "Hamlin", rating: "139.8", cleanAir: "+0.20", timeInRange: "7.4s" },
    { driver: "Elliott", rating: "138.1", cleanAir: "+0.18", timeInRange: "7.9s" },
    { driver: "Blaney", rating: "136.7", cleanAir: "+0.16", timeInRange: "8.2s" },
  ];

  return (
    <div className="flex flex-col gap-8">
      <section className="panel p-8 md:p-12 fade-up">
        <div className="flex flex-col gap-4">
          <span className="chip">NASCAR live</span>
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
                  <th>Fuel</th>
                  <th>Tires</th>
                </tr>
              </thead>
              <tbody>
                {liveStandings.map((row) => (
                  <tr key={row.pos}>
                    <td>{row.pos}</td>
                    <td>{row.driver}</td>
                    <td>#{row.car}</td>
                    <td>{row.gap}</td>
                    <td>{row.fuel}</td>
                    <td>{row.tires}</td>
                  </tr>
                ))}
              </tbody>
            </table>
          </div>
        </div>

        <div className="panel p-6 md:p-8">
          <h2 className="section-title">
            <button
              type="button"
              className="section-title-button"
              onClick={() => setBurnBarOpen(true)}
            >
              Burn bar
            </button>
          </h2>
          <div className="mt-6 space-y-4">
            {burnBar.map((item) => (
              <div key={item.title} className="panel-soft p-4">
                <p className="text-base font-semibold">{item.title}</p>
                <p className="mt-2 text-2xl font-semibold">{item.value}</p>
                <p className="mt-2 text-sm muted">{item.detail}</p>
              </div>
            ))}
          </div>
        </div>
      </section>

      <section className="panel p-6 md:p-8 fade-up delay-2">
        <div className="flex items-center justify-between">
          <h2 className="section-title">
            <button
              type="button"
              className="section-title-button"
              onClick={() => setInsightsOpen(true)}
            >
              Race insights
            </button>
          </h2>
          <span className="muted text-sm">AI assisted metrics</span>
        </div>
        <div className="mt-6 grid gap-4 md:grid-cols-3">
          {insights.map((item) => (
            <div key={item.title} className="panel-soft p-4">
              <p className="text-base font-semibold">{item.title}</p>
              <p className="mt-2 text-sm muted">{item.detail}</p>
            </div>
          ))}
        </div>
      </section>

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
                <th>Fuel</th>
                <th>Tires</th>
                <th>Last lap</th>
                <th>Avg run</th>
                <th>Draft</th>
                <th>Pass rate</th>
              </tr>
            </thead>
            <tbody>
              {fullStandings.map((row) => (
                <tr key={row.pos}
                >
                  <td>{row.pos}</td>
                  <td>{row.driver}</td>
                  <td>#{row.car}</td>
                  <td>{row.gap}</td>
                  <td>{row.fuel}</td>
                  <td>{row.tires}</td>
                  <td>{row.lastLap}</td>
                  <td>{row.avgRun}</td>
                  <td>{row.draft}</td>
                  <td>{row.passRate}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </Modal>

      <Modal
        isOpen={burnBarOpen}
        title="Burn bar analytics"
        onClose={() => setBurnBarOpen(false)}
      >
        <div className="grid gap-4 md:grid-cols-3">
          {burnBarDetails.map((item) => (
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
                <th>Driver</th>
                <th>Burn</th>
                <th>Trend</th>
                <th>Risk</th>
              </tr>
            </thead>
            <tbody>
              {burnTable.map((row) => (
                <tr key={row.driver}>
                  <td>{row.driver}</td>
                  <td>{row.burn}</td>
                  <td>{row.trend}</td>
                  <td>{row.risk}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </Modal>

      <Modal
        isOpen={insightsOpen}
        title="Race insights analytics"
        onClose={() => setInsightsOpen(false)}
      >
        <div className="grid gap-4 md:grid-cols-3">
          {insights.map((item) => (
            <div key={item.title} className="panel-soft p-4">
              <p className="text-base font-semibold">{item.title}</p>
              <p className="mt-2 text-sm muted">{item.detail}</p>
            </div>
          ))}
        </div>
        <div className="panel-soft p-4 overflow-x-auto">
          <table className="data-table">
            <thead>
              <tr>
                <th>Driver</th>
                <th>Rating</th>
                <th>Clean air</th>
                <th>Time in range</th>
              </tr>
            </thead>
            <tbody>
              {insightsTable.map((row) => (
                <tr key={row.driver}>
                  <td>{row.driver}</td>
                  <td>{row.rating}</td>
                  <td>{row.cleanAir}</td>
                  <td>{row.timeInRange}</td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
      </Modal>
    </div>
  );
}
