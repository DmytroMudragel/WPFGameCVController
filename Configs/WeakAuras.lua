
WeakAurasSaved = {
	["dynamicIconCache"] = {
	},
	["editor_tab_spaces"] = 4,
	["displays"] = {
		["Mounted"] = {
			["iconSource"] = -1,
			["xOffset"] = 133,
			["preferToUpdate"] = false,
			["yOffset"] = 0,
			["anchorPoint"] = "CENTER",
			["cooldownSwipe"] = true,
			["cooldownEdge"] = false,
			["icon"] = true,
			["triggers"] = {
				{
					["trigger"] = {
						["itemName"] = 0,
						["use_genericShowOn"] = true,
						["genericShowOn"] = "showOnCooldown",
						["names"] = {
						},
						["specId"] = {
							["single"] = 253,
							["multi"] = {
							},
						},
						["debuffType"] = "HELPFUL",
						["type"] = "unit",
						["subeventSuffix"] = "_CAST_START",
						["use_absorbMode"] = true,
						["event"] = "Conditions",
						["eventtype"] = "PLAYER_REGEN_ENABLED",
						["duration"] = "0",
						["use_itemName"] = true,
						["spellIds"] = {
						},
						["use_eventtype"] = true,
						["use_unit"] = true,
						["use_mounted"] = true,
						["subeventPrefix"] = "SPELL",
						["unit"] = "player",
					},
					["untrigger"] = {
					},
				}, -- [1]
				{
					["trigger"] = {
						["useName"] = false,
						["useExactSpellId"] = true,
						["use_absorbMode"] = true,
						["event"] = "Health",
						["use_unit"] = true,
						["auraspellids"] = {
							"783", -- [1]
						},
						["unit"] = "player",
						["type"] = "aura2",
						["debuffType"] = "HELPFUL",
					},
					["untrigger"] = {
					},
				}, -- [2]
				{
					["trigger"] = {
						["custom_hide"] = "timed",
						["type"] = "custom",
						["events"] = "ACTIONBAR_PAGE_CHANGED",
						["custom_type"] = "event",
						["custom"] = "function()\nreturn true\nend",
						["duration"] = "2",
						["debuffType"] = "HELPFUL",
						["unit"] = "player",
					},
					["untrigger"] = {
					},
				}, -- [3]
				["disjunctive"] = "any",
				["activeTriggerMode"] = -10,
			},
			["internalVersion"] = 45,
			["keepAspectRatio"] = false,
			["animation"] = {
				["start"] = {
					["type"] = "custom",
					["easeStrength"] = 3,
					["duration_type"] = "seconds",
					["easeType"] = "none",
				},
				["main"] = {
					["type"] = "none",
					["easeStrength"] = 3,
					["duration_type"] = "seconds",
					["easeType"] = "none",
				},
				["finish"] = {
					["type"] = "none",
					["easeStrength"] = 3,
					["duration_type"] = "seconds",
					["easeType"] = "none",
				},
			},
			["desaturate"] = false,
			["rotation"] = 0,
			["subRegions"] = {
			},
			["height"] = 15,
			["rotate"] = false,
			["load"] = {
				["use_never"] = false,
				["talent"] = {
					["multi"] = {
					},
				},
				["class"] = {
					["multi"] = {
					},
				},
				["spec"] = {
					["multi"] = {
					},
				},
				["size"] = {
					["multi"] = {
					},
				},
			},
			["textureWrapMode"] = "CLAMPTOBLACKADDITIVE",
			["zoom"] = 0,
			["discrete_rotation"] = 0,
			["mirror"] = false,
			["authorOptions"] = {
			},
			["regionType"] = "texture",
			["selfPoint"] = "TOPLEFT",
			["blendMode"] = "BLEND",
			["cooldown"] = false,
			["actions"] = {
				["start"] = {
				},
				["init"] = {
				},
				["finish"] = {
				},
			},
			["texture"] = "Interface\\AddOns\\WeakAuras\\Media\\Textures\\Square_White",
			["config"] = {
			},
			["cooldownTextDisabled"] = false,
			["color"] = {
				0.450980392156863, -- [1]
				1, -- [2]
				0.83921568627451, -- [3]
				1, -- [4]
			},
			["tocversion"] = 20501,
			["id"] = "Mounted",
			["anchorFrameType"] = "SCREEN",
			["frameStrata"] = 1,
			["width"] = 20,
			["alpha"] = 1,
			["uid"] = "Or2fyDfOAIG",
			["inverse"] = false,
			["useTooltip"] = false,
			["conditions"] = {
			},
			["information"] = {
				["ignoreOptionsEventErrors"] = true,
			},
			["parent"] = "Wow        tg",
		},
		["need to face"] = {
			["authorOptions"] = {
			},
			["preferToUpdate"] = false,
			["yOffset"] = 0,
			["anchorPoint"] = "CENTER",
			["url"] = "",
			["actions"] = {
				["start"] = {
				},
				["init"] = {
				},
				["finish"] = {
				},
			},
			["triggers"] = {
				{
					["trigger"] = {
						["type"] = "custom",
						["unevent"] = "timed",
						["custom_hide"] = "timed",
						["duration"] = "1",
						["event"] = "Combat Log",
						["unit"] = "player",
						["custom_type"] = "event",
						["subeventSuffix"] = "_CAST_START",
						["custom"] = "function(object, event, message)\n    if (message == (\"Target needs to be in front of you.\")) then\n        return true\n    end\n    if (message == (\"You are facing the wrong way!\")) then\n        return true\n    end\nend",
						["subeventPrefix"] = "SPELL",
						["events"] = "UI_ERROR_MESSAGE",
						["spellIds"] = {
						},
						["names"] = {
						},
						["debuffType"] = "HELPFUL",
					},
					["untrigger"] = {
					},
				}, -- [1]
				["activeTriggerMode"] = -10,
			},
			["internalVersion"] = 45,
			["selfPoint"] = "TOPLEFT",
			["desaturate"] = false,
			["rotation"] = 0,
			["version"] = 1,
			["subRegions"] = {
			},
			["height"] = 15,
			["rotate"] = false,
			["load"] = {
				["talent"] = {
					["multi"] = {
					},
				},
				["zoneIds"] = "",
				["class"] = {
					["multi"] = {
					},
				},
				["spec"] = {
					["multi"] = {
					},
				},
				["size"] = {
					["multi"] = {
					},
				},
			},
			["textureWrapMode"] = "CLAMPTOBLACKADDITIVE",
			["mirror"] = false,
			["regionType"] = "texture",
			["blendMode"] = "BLEND",
			["frameStrata"] = 1,
			["texture"] = "Interface\\AddOns\\WeakAuras\\Media\\Textures\\Square_FullWhite",
			["parent"] = "Wow        tg",
			["color"] = {
				1, -- [1]
				0.12549019607843, -- [2]
				0, -- [3]
				1, -- [4]
			},
			["semver"] = "1.0.0",
			["tocversion"] = 20501,
			["id"] = "need to face",
			["animation"] = {
				["start"] = {
					["type"] = "custom",
					["easeStrength"] = 3,
					["duration_type"] = "seconds",
					["easeType"] = "none",
				},
				["main"] = {
					["type"] = "none",
					["easeStrength"] = 3,
					["duration_type"] = "seconds",
					["easeType"] = "none",
				},
				["finish"] = {
					["type"] = "none",
					["easeStrength"] = 3,
					["duration_type"] = "seconds",
					["easeType"] = "none",
				},
			},
			["alpha"] = 1,
			["anchorFrameType"] = "SCREEN",
			["width"] = 20,
			["uid"] = "06vyrFCN(BV",
			["xOffset"] = 247,
			["config"] = {
			},
			["conditions"] = {
			},
			["information"] = {
				["ignoreOptionsEventErrors"] = true,
			},
			["discrete_rotation"] = 0,
		},
		["hp/mana/target hp"] = {
			["xOffset"] = 57,
			["preferToUpdate"] = false,
			["yOffset"] = 0,
			["anchorPoint"] = "CENTER",
			["actions"] = {
				["start"] = {
				},
				["finish"] = {
				},
				["init"] = {
				},
			},
			["triggers"] = {
				{
					["trigger"] = {
						["type"] = "unit",
						["use_alwaystrue"] = true,
						["subeventSuffix"] = "_CAST_START",
						["duration"] = "1",
						["event"] = "Conditions",
						["names"] = {
						},
						["spellIds"] = {
						},
						["unevent"] = "auto",
						["use_unit"] = true,
						["unit"] = "player",
						["subeventPrefix"] = "SPELL",
						["debuffType"] = "HELPFUL",
					},
					["untrigger"] = {
					},
				}, -- [1]
				["disjunctive"] = "any",
				["activeTriggerMode"] = -10,
			},
			["internalVersion"] = 45,
			["animation"] = {
				["start"] = {
					["colorR"] = 1,
					["duration"] = "36000",
					["colorA"] = 1,
					["colorG"] = 1,
					["type"] = "custom",
					["duration_type"] = "seconds",
					["easeType"] = "none",
					["use_scale"] = false,
					["scaley"] = 1,
					["alpha"] = 0,
					["rotate"] = 0,
					["y"] = 0,
					["x"] = 0,
					["use_color"] = true,
					["colorType"] = "custom",
					["colorFunc"] = "function(progress, r1, g1, b1, a1, r2, g2, b2, a2)\n    \n    local combat\n    local power = UnitPower(\"player\")\n    local powermax = UnitPowerMax(\"player\")\n    local percentagepower=math.floor(power/powermax*100)/256\n    local hp_ = UnitHealth(\"player\") \n    local hp_Max=UnitHealthMax(\"player\")\n    local percentagehealth=math.floor(hp_/hp_Max*100)/256\n    local hp_target = UnitHealth(\"target\") \n    local hp_Max_target=UnitHealthMax(\"target\")\n    local percentagehealthtarget=math.floor(hp_target/hp_Max_target*100)/256\n    \n    return percentagehealth,percentagepower, percentagehealthtarget, 1\nend",
					["easeStrength"] = 3,
					["scalex"] = 1,
					["colorB"] = 1,
				},
				["main"] = {
					["duration_type"] = "seconds",
					["type"] = "none",
					["easeStrength"] = 3,
					["easeType"] = "none",
				},
				["finish"] = {
					["duration_type"] = "seconds",
					["type"] = "none",
					["easeStrength"] = 3,
					["easeType"] = "none",
				},
			},
			["desaturate"] = false,
			["discrete_rotation"] = 0,
			["subRegions"] = {
			},
			["height"] = 15,
			["rotate"] = false,
			["load"] = {
				["use_never"] = false,
				["talent"] = {
					["multi"] = {
					},
				},
				["size"] = {
					["multi"] = {
					},
				},
				["spec"] = {
					["multi"] = {
					},
				},
				["class"] = {
					["multi"] = {
					},
				},
				["zoneIds"] = "",
			},
			["textureWrapMode"] = "CLAMPTOBLACKADDITIVE",
			["mirror"] = false,
			["regionType"] = "texture",
			["blendMode"] = "BLEND",
			["texture"] = "Interface\\AddOns\\WeakAuras\\Media\\Textures\\Square_White",
			["anchorFrameType"] = "SCREEN",
			["parent"] = "Wow        tg",
			["rotation"] = 0,
			["tocversion"] = 20501,
			["id"] = "hp/mana/target hp",
			["frameStrata"] = 1,
			["alpha"] = 1,
			["width"] = 20,
			["selfPoint"] = "TOPLEFT",
			["uid"] = "WwgD7YRVeCG",
			["authorOptions"] = {
			},
			["config"] = {
			},
			["conditions"] = {
			},
			["information"] = {
				["ignoreOptionsEventErrors"] = true,
			},
			["color"] = {
				1, -- [1]
				0, -- [2]
				0.298039215686275, -- [3]
				1, -- [4]
			},
		},
		["Whisp"] = {
			["iconSource"] = 0,
			["xOffset"] = 190,
			["preferToUpdate"] = false,
			["yOffset"] = 0,
			["anchorPoint"] = "CENTER",
			["cooldownSwipe"] = true,
			["cooldownEdge"] = false,
			["actions"] = {
				["start"] = {
				},
				["finish"] = {
				},
				["init"] = {
					["do_custom"] = false,
				},
			},
			["triggers"] = {
				{
					["trigger"] = {
						["duration"] = "5",
						["use_spell"] = false,
						["subeventPrefix"] = "SPELL",
						["debuffType"] = "HELPFUL",
						["type"] = "event",
						["use_health"] = false,
						["subeventSuffix"] = "_CAST_START",
						["unit"] = "player",
						["percenthealth"] = "35",
						["event"] = "Chat Message",
						["percenthealth_operator"] = "<=",
						["use_unit"] = true,
						["names"] = {
						},
						["spellIds"] = {
						},
						["use_absorbMode"] = true,
						["use_message"] = false,
						["use_percenthealth"] = true,
						["use_messageType"] = true,
						["messageType"] = "CHAT_MSG_WHISPER",
					},
					["untrigger"] = {
					},
				}, -- [1]
				{
					["trigger"] = {
						["duration"] = "5",
						["use_spell"] = false,
						["unit"] = "player",
						["debuffType"] = "HELPFUL",
						["type"] = "event",
						["use_health"] = false,
						["subeventSuffix"] = "_CAST_START",
						["use_unit"] = true,
						["percenthealth"] = "35",
						["event"] = "Chat Message",
						["use_messageType"] = true,
						["names"] = {
						},
						["use_absorbMode"] = true,
						["spellIds"] = {
						},
						["subeventPrefix"] = "SPELL",
						["use_message"] = false,
						["use_percenthealth"] = true,
						["percenthealth_operator"] = "<=",
						["messageType"] = "CHAT_MSG_BN_WHISPER",
					},
					["untrigger"] = {
					},
				}, -- [2]
				{
					["trigger"] = {
						["custom_hide"] = "timed",
						["type"] = "custom",
						["events"] = "ACTIONBAR_PAGE_CHANGED",
						["custom_type"] = "event",
						["custom"] = "function()\nreturn true\nend",
						["duration"] = "2",
						["debuffType"] = "HELPFUL",
						["unit"] = "player",
					},
					["untrigger"] = {
					},
				}, -- [3]
				["disjunctive"] = "any",
				["activeTriggerMode"] = -10,
			},
			["internalVersion"] = 45,
			["keepAspectRatio"] = false,
			["selfPoint"] = "TOPLEFT",
			["desaturate"] = false,
			["rotation"] = 0,
			["subRegions"] = {
			},
			["height"] = 15,
			["rotate"] = false,
			["load"] = {
				["use_never"] = false,
				["talent"] = {
					["multi"] = {
					},
				},
				["class"] = {
					["multi"] = {
					},
				},
				["spec"] = {
					["multi"] = {
					},
				},
				["size"] = {
					["multi"] = {
					},
				},
			},
			["textureWrapMode"] = "CLAMPTOBLACKADDITIVE",
			["color"] = {
				1, -- [1]
				0.847058823529412, -- [2]
				0.682352941176471, -- [3]
				1, -- [4]
			},
			["authorOptions"] = {
			},
			["mirror"] = false,
			["icon"] = true,
			["regionType"] = "texture",
			["parent"] = "Wow        tg",
			["blendMode"] = "BLEND",
			["information"] = {
				["ignoreOptionsEventErrors"] = true,
			},
			["displayIcon"] = "237538",
			["texture"] = "Interface\\AddOns\\WeakAuras\\Media\\Textures\\Square_White",
			["uid"] = "(c3jMKKFDk)",
			["zoom"] = 0,
			["cooldownTextDisabled"] = false,
			["tocversion"] = 20501,
			["id"] = "Whisp",
			["width"] = 20,
			["alpha"] = 1,
			["anchorFrameType"] = "SCREEN",
			["frameStrata"] = 1,
			["config"] = {
			},
			["inverse"] = false,
			["discrete_rotation"] = 0,
			["conditions"] = {
			},
			["cooldown"] = false,
			["animation"] = {
				["start"] = {
					["easeStrength"] = 3,
					["type"] = "custom",
					["duration_type"] = "seconds",
					["easeType"] = "none",
				},
				["main"] = {
					["easeStrength"] = 3,
					["type"] = "none",
					["duration_type"] = "seconds",
					["easeType"] = "none",
				},
				["finish"] = {
					["easeStrength"] = 3,
					["type"] = "none",
					["duration_type"] = "seconds",
					["easeType"] = "none",
				},
			},
		},
		["Died"] = {
			["iconSource"] = 0,
			["color"] = {
				0.894117647058824, -- [1]
				0.501960784313726, -- [2]
				1, -- [3]
				1, -- [4]
			},
			["preferToUpdate"] = false,
			["yOffset"] = 0,
			["anchorPoint"] = "CENTER",
			["cooldownSwipe"] = true,
			["cooldownEdge"] = false,
			["icon"] = true,
			["triggers"] = {
				{
					["trigger"] = {
						["use_unitisunit"] = false,
						["use_status"] = false,
						["use_absorbMode"] = true,
						["use_character"] = false,
						["names"] = {
						},
						["debuffType"] = "HELPFUL",
						["type"] = "unit",
						["subeventSuffix"] = "_CAST_START",
						["subeventPrefix"] = "SPELL",
						["threatUnit"] = "target",
						["unit"] = "player",
						["use_threatUnit"] = true,
						["event"] = "Conditions",
						["spellIds"] = {
						},
						["unitisunit"] = "target",
						["use_unit"] = true,
						["use_alive"] = true,
						["use_hostility"] = false,
						["use_aggro"] = true,
					},
					["untrigger"] = {
					},
				}, -- [1]
				{
					["trigger"] = {
						["unit"] = "player",
						["type"] = "custom",
						["events"] = "ACTIONBAR_PAGE_CHANGED",
						["custom_type"] = "event",
						["debuffType"] = "HELPFUL",
						["duration"] = "2",
						["custom"] = "function()\nreturn true\nend",
						["custom_hide"] = "timed",
					},
					["untrigger"] = {
					},
				}, -- [2]
				["disjunctive"] = "any",
				["activeTriggerMode"] = -10,
			},
			["internalVersion"] = 45,
			["keepAspectRatio"] = false,
			["selfPoint"] = "TOPLEFT",
			["desaturate"] = false,
			["discrete_rotation"] = 0,
			["subRegions"] = {
			},
			["height"] = 15,
			["rotate"] = false,
			["load"] = {
				["use_never"] = false,
				["talent"] = {
					["multi"] = {
					},
				},
				["class"] = {
					["multi"] = {
					},
				},
				["spec"] = {
					["multi"] = {
					},
				},
				["size"] = {
					["multi"] = {
					},
				},
			},
			["textureWrapMode"] = "CLAMPTOBLACKADDITIVE",
			["xOffset"] = 114,
			["rotation"] = 0,
			["mirror"] = false,
			["actions"] = {
				["start"] = {
				},
				["finish"] = {
				},
				["init"] = {
				},
			},
			["regionType"] = "texture",
			["parent"] = "Wow        tg",
			["blendMode"] = "BLEND",
			["information"] = {
				["ignoreOptionsEventErrors"] = true,
			},
			["displayIcon"] = "237538",
			["texture"] = "Interface\\AddOns\\WeakAuras\\Media\\Textures\\Square_White",
			["uid"] = "RountaCiRDD",
			["zoom"] = 0,
			["cooldownTextDisabled"] = false,
			["tocversion"] = 20501,
			["id"] = "Died",
			["width"] = 20,
			["alpha"] = 1,
			["anchorFrameType"] = "SCREEN",
			["frameStrata"] = 1,
			["config"] = {
			},
			["inverse"] = false,
			["animation"] = {
				["start"] = {
					["easeStrength"] = 3,
					["type"] = "custom",
					["duration_type"] = "seconds",
					["easeType"] = "none",
				},
				["main"] = {
					["easeStrength"] = 3,
					["type"] = "none",
					["duration_type"] = "seconds",
					["easeType"] = "none",
				},
				["finish"] = {
					["easeStrength"] = 3,
					["type"] = "none",
					["duration_type"] = "seconds",
					["easeType"] = "none",
				},
			},
			["conditions"] = {
			},
			["cooldown"] = false,
			["authorOptions"] = {
			},
		},
		["mob id"] = {
			["authorOptions"] = {
			},
			["preferToUpdate"] = false,
			["yOffset"] = 0,
			["anchorPoint"] = "CENTER",
			["actions"] = {
				["start"] = {
				},
				["finish"] = {
				},
				["init"] = {
					["do_custom"] = false,
				},
			},
			["triggers"] = {
				{
					["trigger"] = {
						["type"] = "unit",
						["use_status"] = false,
						["subeventSuffix"] = "_CAST_START",
						["duration"] = "1",
						["event"] = "Unit Characteristics",
						["use_unit"] = true,
						["use_threatUnit"] = true,
						["threatUnit"] = "focus",
						["spellIds"] = {
						},
						["unevent"] = "auto",
						["names"] = {
						},
						["unit"] = "target",
						["subeventPrefix"] = "SPELL",
						["debuffType"] = "HELPFUL",
					},
					["untrigger"] = {
					},
				}, -- [1]
				["disjunctive"] = "any",
				["activeTriggerMode"] = -10,
			},
			["internalVersion"] = 45,
			["selfPoint"] = "TOPLEFT",
			["desaturate"] = false,
			["discrete_rotation"] = 0,
			["subRegions"] = {
			},
			["height"] = 15,
			["rotate"] = false,
			["load"] = {
				["use_never"] = false,
				["talent"] = {
					["multi"] = {
					},
				},
				["size"] = {
					["multi"] = {
					},
				},
				["spec"] = {
					["multi"] = {
					},
				},
				["class"] = {
					["multi"] = {
					},
				},
				["zoneIds"] = "",
			},
			["textureWrapMode"] = "CLAMPTOBLACKADDITIVE",
			["mirror"] = false,
			["regionType"] = "texture",
			["blendMode"] = "BLEND",
			["texture"] = "Interface\\AddOns\\WeakAuras\\Media\\Textures\\Square_White",
			["width"] = 20,
			["parent"] = "Wow        tg",
			["animation"] = {
				["start"] = {
					["colorR"] = 1,
					["use_scale"] = false,
					["colorB"] = 1,
					["colorG"] = 1,
					["scalex"] = 1,
					["type"] = "custom",
					["preset"] = "fade",
					["easeType"] = "none",
					["rotate"] = 0,
					["use_color"] = true,
					["alpha"] = 0,
					["duration_type"] = "seconds",
					["y"] = 0,
					["x"] = 0,
					["colorType"] = "custom",
					["scaley"] = 1,
					["colorFunc"] = "function(progress, r1, g1, b1, a1, r2, g2, b2, a2)\n    local guid = UnitGUID(\"target\")\n    local D = 0\n    local C = 0\n    local M = 0\n    --local type, zero, server_id, instance_id, zone_uid, npc_id, spawn_uid = strsplit(\"-\",guid);\n    local _, _, _, _, _, npc_id, _ = strsplit(\"-\",guid);\n    if strlen(npc_id) == 1 then\n        return 0,0,npc_id/256,1\n    elseif strlen(npc_id) == 2 then\n        return 0,0,npc_id/256,1\n    elseif strlen(npc_id) == 3 then\n        D = strsub(npc_id,2,3)/256\n        C = strsub(npc_id,1,1)/256\n        return M,C,D,1\n    elseif strlen(npc_id) == 4 then\n        D = strsub(npc_id,3,4)/256\n        C = strsub(npc_id,1,2)/256\n        return M,C,D,1\n    elseif strlen(npc_id) == 5 then\n        D = strsub(npc_id,4,5)/256\n        C = strsub(npc_id,2,3)/256\n        M = strsub(npc_id,1,1)/256\n        return M,C,D,1\n    elseif strlen(npc_id) == 6 then\n        D = strsub(npc_id,5,6)/256\n        C = strsub(npc_id,3,4)/256\n        M = strsub(npc_id,1,2)/256\n        return M,C,D,1\n    end\nend\n\n\n\n",
					["easeStrength"] = 3,
					["duration"] = "999999999",
					["colorA"] = 1,
				},
				["main"] = {
					["duration_type"] = "seconds",
					["type"] = "none",
					["easeStrength"] = 3,
					["easeType"] = "none",
				},
				["finish"] = {
					["duration_type"] = "seconds",
					["type"] = "none",
					["easeStrength"] = 3,
					["easeType"] = "none",
				},
			},
			["tocversion"] = 20501,
			["id"] = "mob id",
			["alpha"] = 1,
			["frameStrata"] = 1,
			["anchorFrameType"] = "SCREEN",
			["uid"] = "zKJJnPiWbqh",
			["config"] = {
			},
			["xOffset"] = 266,
			["color"] = {
				1, -- [1]
				0, -- [2]
				0.298039215686275, -- [3]
				1, -- [4]
			},
			["conditions"] = {
			},
			["information"] = {
				["ignoreOptionsEventErrors"] = true,
			},
			["rotation"] = 0,
		},
		["money count"] = {
			["authorOptions"] = {
			},
			["preferToUpdate"] = false,
			["yOffset"] = 0,
			["anchorPoint"] = "CENTER",
			["actions"] = {
				["start"] = {
				},
				["finish"] = {
				},
				["init"] = {
					["do_custom"] = false,
				},
			},
			["triggers"] = {
				{
					["trigger"] = {
						["type"] = "unit",
						["use_status"] = false,
						["subeventSuffix"] = "_CAST_START",
						["threatUnit"] = "focus",
						["duration"] = "1",
						["event"] = "Conditions",
						["subeventPrefix"] = "SPELL",
						["use_threatUnit"] = true,
						["use_alwaystrue"] = true,
						["spellIds"] = {
						},
						["unevent"] = "auto",
						["use_unit"] = true,
						["names"] = {
						},
						["unit"] = "target",
						["debuffType"] = "HELPFUL",
					},
					["untrigger"] = {
					},
				}, -- [1]
				["disjunctive"] = "any",
				["activeTriggerMode"] = -10,
			},
			["internalVersion"] = 45,
			["selfPoint"] = "TOPLEFT",
			["desaturate"] = false,
			["discrete_rotation"] = 0,
			["subRegions"] = {
			},
			["height"] = 15,
			["rotate"] = false,
			["load"] = {
				["use_never"] = false,
				["talent"] = {
					["multi"] = {
					},
				},
				["size"] = {
					["multi"] = {
					},
				},
				["spec"] = {
					["multi"] = {
					},
				},
				["class"] = {
					["multi"] = {
					},
				},
				["zoneIds"] = "",
			},
			["textureWrapMode"] = "CLAMPTOBLACKADDITIVE",
			["mirror"] = false,
			["regionType"] = "texture",
			["blendMode"] = "BLEND",
			["texture"] = "Interface\\AddOns\\WeakAuras\\Media\\Textures\\Square_White",
			["width"] = 20,
			["parent"] = "Wow        tg",
			["animation"] = {
				["start"] = {
					["colorR"] = 1,
					["use_scale"] = false,
					["colorB"] = 1,
					["colorG"] = 1,
					["scalex"] = 1,
					["type"] = "custom",
					["preset"] = "fade",
					["easeType"] = "none",
					["rotate"] = 0,
					["use_color"] = true,
					["alpha"] = 0,
					["duration_type"] = "seconds",
					["y"] = 0,
					["x"] = 0,
					["colorType"] = "custom",
					["scaley"] = 1,
					["colorFunc"] = "function(progress, r1, g1, b1, a1, r2, g2, b2, a2)\n    \n    local money = GetMoney() / 10000\n    \n    local hash3 = money % 256\n    local hash2 = ((money - hash3) / 256) % 256\n    local hash1 = math.floor(((money - hash3) / 256) / 256)\n    local hash3conv = hash3 / 256.\n    local hash2conv = hash2 / 256.\n    local hash1conv = hash1 / 256.\n    \n    return hash1conv, hash2conv, hash3conv, 1\nend",
					["easeStrength"] = 3,
					["duration"] = "999999999",
					["colorA"] = 1,
				},
				["main"] = {
					["duration_type"] = "seconds",
					["type"] = "none",
					["easeStrength"] = 3,
					["easeType"] = "none",
				},
				["finish"] = {
					["duration_type"] = "seconds",
					["type"] = "none",
					["easeStrength"] = 3,
					["easeType"] = "none",
				},
			},
			["tocversion"] = 20501,
			["id"] = "money count",
			["alpha"] = 1,
			["frameStrata"] = 1,
			["anchorFrameType"] = "SCREEN",
			["uid"] = "VMRz)hP5XHx",
			["config"] = {
			},
			["xOffset"] = 304,
			["color"] = {
				1, -- [1]
				0, -- [2]
				0.298039215686275, -- [3]
				1, -- [4]
			},
			["conditions"] = {
			},
			["information"] = {
				["ignoreOptionsEventErrors"] = true,
			},
			["rotation"] = 0,
		},
		["angle"] = {
			["xOffset"] = 38,
			["preferToUpdate"] = false,
			["yOffset"] = 0,
			["anchorPoint"] = "TOPLEFT",
			["actions"] = {
				["start"] = {
				},
				["finish"] = {
				},
				["init"] = {
					["do_custom"] = false,
				},
			},
			["triggers"] = {
				{
					["trigger"] = {
						["type"] = "unit",
						["use_alwaystrue"] = true,
						["unevent"] = "auto",
						["duration"] = "1",
						["event"] = "Conditions",
						["names"] = {
						},
						["spellIds"] = {
						},
						["subeventSuffix"] = "_CAST_START",
						["unit"] = "player",
						["subeventPrefix"] = "SPELL",
						["use_unit"] = true,
						["debuffType"] = "HELPFUL",
					},
					["untrigger"] = {
					},
				}, -- [1]
				["disjunctive"] = "any",
				["activeTriggerMode"] = -10,
			},
			["internalVersion"] = 45,
			["selfPoint"] = "TOPLEFT",
			["desaturate"] = false,
			["discrete_rotation"] = 0,
			["subRegions"] = {
			},
			["height"] = 15,
			["rotate"] = false,
			["load"] = {
				["use_never"] = false,
				["talent"] = {
					["multi"] = {
					},
				},
				["size"] = {
					["multi"] = {
					},
				},
				["spec"] = {
					["multi"] = {
					},
				},
				["class"] = {
					["multi"] = {
					},
				},
				["zoneIds"] = "",
			},
			["textureWrapMode"] = "CLAMPTOBLACKADDITIVE",
			["mirror"] = false,
			["regionType"] = "texture",
			["blendMode"] = "BLEND",
			["texture"] = "Interface\\AddOns\\WeakAuras\\Media\\Textures\\Square_White",
			["anchorFrameType"] = "SCREEN",
			["parent"] = "Wow        tg",
			["rotation"] = 0,
			["tocversion"] = 20501,
			["id"] = "angle",
			["frameStrata"] = 1,
			["alpha"] = 1,
			["width"] = 20,
			["animation"] = {
				["start"] = {
					["colorR"] = 1,
					["duration"] = "999999999",
					["colorB"] = 1,
					["colorG"] = 1,
					["use_scale"] = false,
					["type"] = "custom",
					["scalex"] = 1,
					["easeType"] = "none",
					["rotate"] = 0,
					["preset"] = "fade",
					["alpha"] = 0,
					["duration_type"] = "seconds",
					["y"] = 0,
					["x"] = 0,
					["colorType"] = "custom",
					["scaley"] = 1,
					["colorFunc"] = "function(progress, r1, g1, b1, a1, r2, g2, b2, a2)\n    \n    local angle = math.floor(GetPlayerFacing() / 3.14 * 180)\n    local firstdigit = math.floor(angle / 100) / 256.\n    local othertwodigits = math.floor(angle - math.floor(angle / 100)*100 ) / 256.\n    \n    return firstdigit, othertwodigits , 0, 1\nend",
					["easeStrength"] = 3,
					["use_color"] = true,
					["colorA"] = 1,
				},
				["main"] = {
					["duration_type"] = "seconds",
					["type"] = "none",
					["easeStrength"] = 3,
					["easeType"] = "none",
				},
				["finish"] = {
					["duration_type"] = "seconds",
					["type"] = "none",
					["easeStrength"] = 3,
					["easeType"] = "none",
				},
			},
			["uid"] = "Z1L5hMOdTwO",
			["authorOptions"] = {
			},
			["config"] = {
			},
			["conditions"] = {
			},
			["information"] = {
				["ignoreOptionsEventErrors"] = true,
			},
			["color"] = {
				1, -- [1]
				0, -- [2]
				0.298039215686275, -- [3]
				1, -- [4]
			},
		},
		["Low Bag Space"] = {
			["iconSource"] = 0,
			["authorOptions"] = {
			},
			["preferToUpdate"] = false,
			["customText"] = "function()\n    local t=GetTime()\n    aura_env.e=aura_env.e and aura_env.e+t or 0\n    if aura_env.e<1 then return aura_env.d end\n    aura_env.e=0\n    if not aura_env.free then aura_env.free=0 end\n    local green=string.format('%02x',tostring(math.min(aura_env.free*(255/10),255)))\n    aura_env.d='|cffff'..green..'00'..aura_env.free..'|r'\n    return aura_env.d\nend",
			["yOffset"] = 0,
			["anchorPoint"] = "CENTER",
			["cooldownSwipe"] = true,
			["cooldownEdge"] = false,
			["icon"] = true,
			["triggers"] = {
				{
					["trigger"] = {
						["type"] = "custom",
						["custom_type"] = "event",
						["debuffType"] = "HELPFUL",
						["event"] = "Health",
						["subeventPrefix"] = "SPELL",
						["subeventSuffix"] = "_CAST_START",
						["spellIds"] = {
						},
						["events"] = "BAG_UPDATE",
						["names"] = {
						},
						["check"] = "event",
						["custom"] = "function()\n    aura_env.free=0\n    for bag=0,NUM_BAG_SLOTS do\n        local free=tonumber((GetContainerNumFreeSlots(bag)))\n        aura_env.free=free and aura_env.free+free or aura_env.free\n    end\n    return aura_env.free<5\nend",
						["unit"] = "player",
						["custom_hide"] = "custom",
					},
					["untrigger"] = {
						["custom"] = "function()\n    return aura_env.free>1\nend",
					},
				}, -- [1]
				{
					["trigger"] = {
						["custom_hide"] = "timed",
						["type"] = "custom",
						["events"] = "ACTIONBAR_PAGE_CHANGED",
						["custom_type"] = "event",
						["custom"] = "function()\nreturn true\nend",
						["duration"] = "2",
						["debuffType"] = "HELPFUL",
						["unit"] = "player",
					},
					["untrigger"] = {
					},
				}, -- [2]
				["disjunctive"] = "any",
				["activeTriggerMode"] = 1,
			},
			["internalVersion"] = 45,
			["keepAspectRatio"] = false,
			["animation"] = {
				["start"] = {
					["type"] = "custom",
					["duration_type"] = "seconds",
					["easeStrength"] = 3,
					["easeType"] = "none",
				},
				["main"] = {
					["type"] = "none",
					["duration_type"] = "seconds",
					["easeStrength"] = 3,
					["easeType"] = "none",
				},
				["finish"] = {
					["type"] = "none",
					["duration_type"] = "seconds",
					["easeStrength"] = 3,
					["easeType"] = "none",
				},
			},
			["color"] = {
				0.745098039215686, -- [1]
				0.27843137254902, -- [2]
				0.443137254901961, -- [3]
				1, -- [4]
			},
			["desaturate"] = false,
			["discrete_rotation"] = 0,
			["alpha"] = 1,
			["version"] = 2,
			["subRegions"] = {
			},
			["height"] = 15,
			["rotate"] = false,
			["load"] = {
				["use_never"] = false,
				["talent"] = {
					["multi"] = {
					},
				},
				["class"] = {
					["multi"] = {
					},
				},
				["difficulty"] = {
					["multi"] = {
					},
				},
				["role"] = {
					["multi"] = {
					},
				},
				["zoneIds"] = "",
				["faction"] = {
					["multi"] = {
					},
				},
				["spec"] = {
					["multi"] = {
					},
				},
				["race"] = {
					["multi"] = {
					},
				},
				["pvptalent"] = {
					["multi"] = {
					},
				},
				["size"] = {
					["multi"] = {
					},
				},
			},
			["information"] = {
				["ignoreOptionsEventErrors"] = true,
			},
			["xOffset"] = 171,
			["fontSize"] = 64,
			["displayStacks"] = "%c",
			["stacksPoint"] = "CENTER",
			["conditions"] = {
			},
			["url"] = "",
			["mirror"] = false,
			["parent"] = "Wow        tg",
			["regionType"] = "texture",
			["uid"] = "xWP7ItA66GT",
			["blendMode"] = "BLEND",
			["actions"] = {
				["start"] = {
				},
				["init"] = {
				},
				["finish"] = {
				},
			},
			["anchorFrameType"] = "SCREEN",
			["texture"] = "Interface\\AddOns\\WeakAuras\\Media\\Textures\\Square_White",
			["semver"] = "1.0.0",
			["zoom"] = 0,
			["auto"] = false,
			["tocversion"] = 20501,
			["id"] = "Low Bag Space",
			["cooldownTextDisabled"] = false,
			["frameStrata"] = 1,
			["width"] = 20,
			["textureWrapMode"] = "CLAMPTOBLACKADDITIVE",
			["config"] = {
			},
			["inverse"] = false,
			["selfPoint"] = "TOPLEFT",
			["displayIcon"] = 133633,
			["cooldown"] = false,
			["rotation"] = 0,
		},
		["Aggro"] = {
			["iconSource"] = 0,
			["xOffset"] = 76,
			["preferToUpdate"] = false,
			["yOffset"] = 0,
			["anchorPoint"] = "CENTER",
			["cooldownSwipe"] = true,
			["cooldownEdge"] = false,
			["actions"] = {
				["start"] = {
				},
				["init"] = {
				},
				["finish"] = {
				},
			},
			["triggers"] = {
				{
					["trigger"] = {
						["use_unitisunit"] = false,
						["use_aggro"] = true,
						["use_absorbMode"] = true,
						["use_character"] = false,
						["subeventPrefix"] = "SPELL",
						["debuffType"] = "HELPFUL",
						["type"] = "unit",
						["subeventSuffix"] = "_CAST_START",
						["event"] = "Threat Situation",
						["use_hostility"] = false,
						["use_threatUnit"] = true,
						["threatUnit"] = "target",
						["spellIds"] = {
						},
						["unitisunit"] = "target",
						["use_status"] = false,
						["use_unit"] = true,
						["unit"] = "player",
						["names"] = {
						},
					},
					["untrigger"] = {
					},
				}, -- [1]
				{
					["trigger"] = {
						["custom_hide"] = "timed",
						["type"] = "custom",
						["events"] = "ACTIONBAR_PAGE_CHANGED",
						["custom_type"] = "event",
						["custom"] = "function()\nreturn true\nend",
						["duration"] = "2",
						["debuffType"] = "HELPFUL",
						["unit"] = "player",
					},
					["untrigger"] = {
					},
				}, -- [2]
				["disjunctive"] = "any",
				["activeTriggerMode"] = -10,
			},
			["internalVersion"] = 45,
			["keepAspectRatio"] = false,
			["selfPoint"] = "TOPLEFT",
			["desaturate"] = false,
			["rotation"] = 0,
			["subRegions"] = {
			},
			["height"] = 15,
			["rotate"] = false,
			["load"] = {
				["use_never"] = false,
				["talent"] = {
					["multi"] = {
					},
				},
				["class"] = {
					["multi"] = {
					},
				},
				["spec"] = {
					["multi"] = {
					},
				},
				["size"] = {
					["multi"] = {
					},
				},
			},
			["textureWrapMode"] = "CLAMPTOBLACKADDITIVE",
			["color"] = {
				0.894117647058824, -- [1]
				0.501960784313726, -- [2]
				1, -- [3]
				1, -- [4]
			},
			["authorOptions"] = {
			},
			["mirror"] = false,
			["icon"] = true,
			["regionType"] = "texture",
			["parent"] = "Wow        tg",
			["blendMode"] = "BLEND",
			["cooldown"] = false,
			["conditions"] = {
			},
			["texture"] = "Interface\\AddOns\\WeakAuras\\Media\\Textures\\Square_White",
			["config"] = {
			},
			["zoom"] = 0,
			["cooldownTextDisabled"] = false,
			["tocversion"] = 20501,
			["id"] = "Aggro",
			["width"] = 20,
			["alpha"] = 1,
			["anchorFrameType"] = "SCREEN",
			["frameStrata"] = 1,
			["uid"] = "rjxyb0pnJ3F",
			["inverse"] = false,
			["discrete_rotation"] = 0,
			["displayIcon"] = "237538",
			["information"] = {
				["ignoreOptionsEventErrors"] = true,
			},
			["animation"] = {
				["start"] = {
					["type"] = "custom",
					["easeStrength"] = 3,
					["duration_type"] = "seconds",
					["easeType"] = "none",
				},
				["main"] = {
					["type"] = "none",
					["easeStrength"] = 3,
					["duration_type"] = "seconds",
					["easeType"] = "none",
				},
				["finish"] = {
					["type"] = "none",
					["easeStrength"] = 3,
					["duration_type"] = "seconds",
					["easeType"] = "none",
				},
			},
		},
		["Target Distance"] = {
			["iconSource"] = 0,
			["xOffset"] = 95,
			["displayText"] = "%c",
			["yOffset"] = 0,
			["anchorPoint"] = "CENTER",
			["sameTexture"] = true,
			["hideBackground"] = true,
			["icon"] = true,
			["customForegroundRows"] = 16,
			["frameRate"] = 15,
			["keepAspectRatio"] = false,
			["wordWrap"] = "WordWrap",
			["customBackgroundRows"] = 16,
			["desaturate"] = false,
			["rotation"] = 0,
			["font"] = "ArchivoNarrow-Bold",
			["load"] = {
				["use_never"] = false,
				["talent"] = {
					["multi"] = {
					},
				},
				["size"] = {
					["multi"] = {
					},
				},
				["spec"] = {
					["multi"] = {
					},
				},
				["class"] = {
					["multi"] = {
					},
				},
				["zoneIds"] = "",
			},
			["textureWrapMode"] = "CLAMPTOBLACKADDITIVE",
			["foregroundTexture"] = "Interface\\AddOns\\WeakAuras\\Media\\Textures\\stopmotion",
			["shadowXOffset"] = 1,
			["regionType"] = "texture",
			["blendMode"] = "BLEND",
			["texture"] = "Interface\\AddOns\\WeakAuras\\Media\\Textures\\Square_White",
			["zoom"] = 0,
			["customBackgroundFrames"] = 0,
			["alpha"] = 1,
			["uid"] = "rYVBmFZ4zVu",
			["fixedWidth"] = 200,
			["outline"] = "OUTLINE",
			["color"] = {
				0.541176470588235, -- [1]
				0.592156862745098, -- [2]
				1, -- [3]
				1, -- [4]
			},
			["customText"] = "function()\n    \n    if UnitIsDeadOrGhost(\"target\") then return \"\" end\n    \n    local time = GetTime();\n    \n    if(time > aura_env.lastRangeCheck + aura_env.rangeCheckUpdateRate)then\n        aura_env.lastRangeCheck = time;\n        \n        aura_env.rangeCheck()\n        \n        if aura_env.min < 10 then aura_env.color = \"|c0000ff00\"\n        elseif aura_env.min < 20 then aura_env.color = \"|c00aaff00\"\n        elseif aura_env.min < 30 then aura_env.color = \"|c00ffff00\"\n        elseif aura_env.min < 40 then aura_env.color = \"|c00ff8000\"\n        else\n            aura_env.color = \"|c00ff0000\"\n        end\n    end\n    \n    return aura_env.color .. aura_env.min .. \" - \" .. aura_env.max\nend",
			["shadowYOffset"] = -1,
			["desaturateBackground"] = false,
			["cooldownSwipe"] = true,
			["customTextUpdate"] = "update",
			["cooldownEdge"] = false,
			["desaturateForeground"] = false,
			["triggers"] = {
				{
					["trigger"] = {
						["itemName"] = 0,
						["use_hostility"] = false,
						["use_absorbMode"] = true,
						["genericShowOn"] = "showOnCooldown",
						["names"] = {
						},
						["duration"] = "1",
						["character"] = "npc",
						["use_aggro"] = false,
						["use_character"] = true,
						["use_unit"] = true,
						["use_genericShowOn"] = true,
						["use_attackable"] = true,
						["debuffType"] = "HELPFUL",
						["classification"] = {
						},
						["type"] = "unit",
						["unevent"] = "auto",
						["subeventSuffix"] = "_CAST_START",
						["realSpellName"] = 0,
						["event"] = "Unit Characteristics",
						["threatUnit"] = "target",
						["use_itemName"] = true,
						["use_threatUnit"] = true,
						["use_spellName"] = true,
						["spellIds"] = {
						},
						["spellName"] = 0,
						["unit"] = "target",
						["subeventPrefix"] = "SPELL",
						["use_track"] = true,
						["use_unitisunit"] = false,
					},
					["untrigger"] = {
						["unit"] = "target",
					},
				}, -- [1]
				{
					["trigger"] = {
						["type"] = "unit",
						["use_alwaystrue"] = true,
						["use_unit"] = true,
						["debuffType"] = "HELPFUL",
						["event"] = "Conditions",
						["unit"] = "player",
					},
					["untrigger"] = {
					},
				}, -- [2]
				["disjunctive"] = "any",
				["activeTriggerMode"] = -10,
			},
			["internalVersion"] = 45,
			["animation"] = {
				["start"] = {
					["colorR"] = 1,
					["scalex"] = 1,
					["colorB"] = 1,
					["colorG"] = 1,
					["type"] = "custom",
					["easeType"] = "none",
					["duration"] = "36000",
					["scaley"] = 1,
					["alpha"] = 0,
					["duration_type"] = "seconds",
					["y"] = 0,
					["x"] = 0,
					["easeStrength"] = 3,
					["colorA"] = 1,
					["colorFunc"] = "function(progress, r1, g1, b1, a1, r2, g2, b2, a2)\n    aura_env.rangeCheck()\n    local range\n    if aura_env.min == nil\n    then\n        range=60\n    else range = aura_env.min\n    end\n    return (range)/256, 0, 0, 1\nend",
					["rotate"] = 0,
					["colorType"] = "custom",
					["use_color"] = true,
				},
				["main"] = {
					["type"] = "none",
					["easeStrength"] = 3,
					["duration_type"] = "seconds",
					["easeType"] = "none",
				},
				["finish"] = {
					["type"] = "none",
					["easeStrength"] = 3,
					["duration_type"] = "seconds",
					["easeType"] = "none",
				},
			},
			["discrete_rotation"] = 0,
			["selfPoint"] = "TOPLEFT",
			["version"] = 1,
			["subRegions"] = {
			},
			["height"] = 15,
			["rotate"] = false,
			["parent"] = "Wow        tg",
			["cooldown"] = false,
			["fontSize"] = 22,
			["customBackgroundColumns"] = 16,
			["authorOptions"] = {
				{
					["softMin"] = 0,
					["type"] = "range",
					["bigStep"] = 0.05,
					["max"] = 1,
					["step"] = 0.05,
					["width"] = 1,
					["min"] = 0,
					["key"] = "rangeCheckUpdateRate",
					["desc"] = "Time in seconds; 0 means everyframe",
					["softMax"] = 1,
					["useDesc"] = true,
					["name"] = "Range check update rate",
					["default"] = 0.25,
				}, -- [1]
			},
			["backgroundPercent"] = 1,
			["automaticWidth"] = "Auto",
			["mirror"] = false,
			["url"] = "",
			["cooldownTextDisabled"] = false,
			["backgroundColor"] = {
				0.5, -- [1]
				0.5, -- [2]
				0.5, -- [3]
				0.5, -- [4]
			},
			["endPercent"] = 1,
			["tocversion"] = 20501,
			["width"] = 20,
			["customForegroundColumns"] = 16,
			["actions"] = {
				["start"] = {
				},
				["init"] = {
					["custom"] = "aura_env.min = 0\naura_env.max = 0\naura_env.lastRangeCheck = 0;\naura_env.rangeCheckUpdateRate = aura_env.config.rangeCheckUpdateRate;\naura_env.color = \"\"\n\nfunction aura_env.rangeCheck() \n    aura_env.min, aura_env.max = WeakAuras.GetRange(\"target\")\nend",
					["do_custom"] = true,
				},
				["finish"] = {
				},
			},
			["foregroundColor"] = {
				1, -- [1]
				1, -- [2]
				1, -- [3]
				1, -- [4]
			},
			["backgroundTexture"] = "Interface\\AddOns\\WeakAuras\\Media\\Textures\\stopmotion",
			["semver"] = "1.0.0",
			["justify"] = "LEFT",
			["id"] = "Target Distance",
			["startPercent"] = 0,
			["frameStrata"] = 1,
			["anchorFrameType"] = "SCREEN",
			["preferToUpdate"] = false,
			["animationType"] = "loop",
			["inverse"] = false,
			["config"] = {
				["rangeCheckUpdateRate"] = 0,
			},
			["shadowColor"] = {
				0, -- [1]
				0, -- [2]
				0, -- [3]
				1, -- [4]
			},
			["conditions"] = {
			},
			["information"] = {
				["ignoreOptionsEventErrors"] = true,
			},
			["customForegroundFrames"] = 0,
		},
		["failed attempt"] = {
			["color"] = {
				1, -- [1]
				0.12549019607843, -- [2]
				0, -- [3]
				1, -- [4]
			},
			["preferToUpdate"] = false,
			["yOffset"] = 0,
			["anchorPoint"] = "CENTER",
			["url"] = "",
			["actions"] = {
				["start"] = {
				},
				["finish"] = {
				},
				["init"] = {
				},
			},
			["triggers"] = {
				{
					["trigger"] = {
						["type"] = "custom",
						["unevent"] = "timed",
						["debuffType"] = "HELPFUL",
						["duration"] = "1",
						["event"] = "Combat Log",
						["unit"] = "player",
						["names"] = {
						},
						["spellIds"] = {
						},
						["custom"] = "function(object, event, message)\n    if (message == (\"Failed attempt\")) then\n        return true\n    end\nend",
						["events"] = "UI_ERROR_MESSAGE",
						["subeventPrefix"] = "SPELL",
						["subeventSuffix"] = "_CAST_START",
						["custom_type"] = "event",
						["custom_hide"] = "timed",
					},
					["untrigger"] = {
					},
				}, -- [1]
				["activeTriggerMode"] = -10,
			},
			["internalVersion"] = 45,
			["selfPoint"] = "TOPLEFT",
			["desaturate"] = false,
			["rotation"] = 0,
			["version"] = 1,
			["subRegions"] = {
			},
			["height"] = 15,
			["rotate"] = false,
			["load"] = {
				["talent"] = {
					["multi"] = {
					},
				},
				["size"] = {
					["multi"] = {
					},
				},
				["class"] = {
					["multi"] = {
					},
				},
				["spec"] = {
					["multi"] = {
					},
				},
				["zoneIds"] = "",
			},
			["textureWrapMode"] = "CLAMPTOBLACKADDITIVE",
			["mirror"] = false,
			["regionType"] = "texture",
			["blendMode"] = "BLEND",
			["alpha"] = 1,
			["texture"] = "Interface\\AddOns\\WeakAuras\\Media\\Textures\\Square_FullWhite",
			["discrete_rotation"] = 0,
			["parent"] = "Wow        tg",
			["semver"] = "1.0.0",
			["tocversion"] = 20501,
			["id"] = "failed attempt",
			["animation"] = {
				["start"] = {
					["easeStrength"] = 3,
					["type"] = "custom",
					["duration_type"] = "seconds",
					["easeType"] = "none",
				},
				["main"] = {
					["easeStrength"] = 3,
					["type"] = "none",
					["duration_type"] = "seconds",
					["easeType"] = "none",
				},
				["finish"] = {
					["easeStrength"] = 3,
					["type"] = "none",
					["duration_type"] = "seconds",
					["easeType"] = "none",
				},
			},
			["frameStrata"] = 1,
			["anchorFrameType"] = "SCREEN",
			["config"] = {
			},
			["uid"] = "bI8ssqPId4C",
			["xOffset"] = 361,
			["width"] = 20,
			["conditions"] = {
			},
			["information"] = {
				["ignoreOptionsEventErrors"] = true,
			},
			["authorOptions"] = {
			},
		},
		["y"] = {
			["xOffset"] = 19,
			["preferToUpdate"] = false,
			["yOffset"] = 0,
			["anchorPoint"] = "TOPLEFT",
			["actions"] = {
				["start"] = {
				},
				["init"] = {
					["do_custom"] = false,
				},
				["finish"] = {
				},
			},
			["triggers"] = {
				{
					["trigger"] = {
						["type"] = "unit",
						["use_alwaystrue"] = true,
						["unevent"] = "auto",
						["duration"] = "1",
						["event"] = "Conditions",
						["use_unit"] = true,
						["spellIds"] = {
						},
						["subeventSuffix"] = "_CAST_START",
						["subeventPrefix"] = "SPELL",
						["unit"] = "player",
						["names"] = {
						},
						["debuffType"] = "HELPFUL",
					},
					["untrigger"] = {
					},
				}, -- [1]
				["disjunctive"] = "any",
				["activeTriggerMode"] = -10,
			},
			["internalVersion"] = 45,
			["animation"] = {
				["start"] = {
					["colorR"] = 1,
					["scalex"] = 1,
					["colorA"] = 1,
					["colorG"] = 1,
					["type"] = "custom",
					["colorB"] = 1,
					["easeType"] = "none",
					["duration"] = "999999999",
					["scaley"] = 1,
					["alpha"] = 0,
					["easeStrength"] = 3,
					["y"] = 0,
					["x"] = 0,
					["use_color"] = true,
					["colorType"] = "custom",
					["colorFunc"] = "function(progress, r1, g1, b1, a1, r2, g2, b2, a2)\n    \n    local x,y=C_Map.GetPlayerMapPosition(C_Map.GetBestMapForUnit(\"player\"),\"player\"):GetXY()\n    local yi = 0\n    local yf = 0\n    if (y ~= nil)\n    then\n        yi = math.floor(y*100)/256\n        yf = math.floor((y*100 -  math.floor(y*100))*100)/256.\n    end\n    return yi, yf, 0, 1\n    end",
					["rotate"] = 0,
					["duration_type"] = "seconds",
					["use_scale"] = false,
				},
				["main"] = {
					["type"] = "none",
					["duration_type"] = "seconds",
					["easeStrength"] = 3,
					["easeType"] = "none",
				},
				["finish"] = {
					["type"] = "none",
					["duration_type"] = "seconds",
					["easeStrength"] = 3,
					["easeType"] = "none",
				},
			},
			["desaturate"] = false,
			["rotation"] = 0,
			["subRegions"] = {
			},
			["height"] = 15,
			["rotate"] = false,
			["load"] = {
				["use_never"] = false,
				["talent"] = {
					["multi"] = {
					},
				},
				["zoneIds"] = "",
				["spec"] = {
					["multi"] = {
					},
				},
				["class"] = {
					["multi"] = {
					},
				},
				["size"] = {
					["multi"] = {
					},
				},
			},
			["textureWrapMode"] = "CLAMPTOBLACKADDITIVE",
			["mirror"] = false,
			["regionType"] = "texture",
			["blendMode"] = "BLEND",
			["anchorFrameParent"] = false,
			["texture"] = "Interface\\AddOns\\WeakAuras\\Media\\Textures\\Square_White",
			["width"] = 20,
			["color"] = {
				1, -- [1]
				0, -- [2]
				0.298039215686275, -- [3]
				1, -- [4]
			},
			["discrete_rotation"] = 0,
			["tocversion"] = 20501,
			["id"] = "y",
			["frameStrata"] = 1,
			["alpha"] = 1,
			["anchorFrameType"] = "SCREEN",
			["parent"] = "Wow        tg",
			["config"] = {
			},
			["uid"] = "teQb1TlLTmP",
			["authorOptions"] = {
			},
			["conditions"] = {
			},
			["information"] = {
				["ignoreOptionsEventErrors"] = true,
			},
			["selfPoint"] = "TOPLEFT",
		},
		["mobtapped/mobdead/spellrange"] = {
			["parent"] = "Wow        tg",
			["preferToUpdate"] = false,
			["yOffset"] = 0,
			["anchorPoint"] = "CENTER",
			["actions"] = {
				["start"] = {
				},
				["init"] = {
				},
				["finish"] = {
				},
			},
			["triggers"] = {
				{
					["trigger"] = {
						["type"] = "unit",
						["subeventSuffix"] = "_CAST_START",
						["duration"] = "1",
						["event"] = "Unit Characteristics",
						["unit"] = "target",
						["use_threatUnit"] = true,
						["threatUnit"] = "target",
						["spellIds"] = {
						},
						["unevent"] = "auto",
						["use_unit"] = true,
						["names"] = {
						},
						["subeventPrefix"] = "SPELL",
						["debuffType"] = "HELPFUL",
					},
					["untrigger"] = {
					},
				}, -- [1]
				["disjunctive"] = "any",
				["activeTriggerMode"] = -10,
			},
			["internalVersion"] = 45,
			["animation"] = {
				["start"] = {
					["colorR"] = 1,
					["duration_type"] = "seconds",
					["colorA"] = 1,
					["colorG"] = 1,
					["type"] = "custom",
					["colorB"] = 1,
					["easeType"] = "none",
					["scalex"] = 1,
					["use_color"] = true,
					["alpha"] = 0,
					["easeStrength"] = 3,
					["y"] = 0,
					["x"] = 0,
					["scaley"] = 1,
					["colorType"] = "custom",
					["colorFunc"] = "function(progress, r1, g1, b1, a1, r2, g2, b2, a2)\n    local DeadP = 0\n    local isDeadPlayer = UnitIsDead(\"target\")\n    if isDeadPlayer then DeadP = 1 end\n    local tapped = 0\n    local istapped = UnitIsTapDenied(\"target\")\n    if istapped == true then tapped = 1 end\n    local isrange = 0\n    if IsSpellInRange(\"Fire Blast\",\"target\") == 1 then isrange = 1 end\n    \n    return DeadP/256, tapped/256, isrange/256, 1\nend",
					["rotate"] = 0,
					["duration"] = "99999",
					["use_scale"] = false,
				},
				["main"] = {
					["type"] = "none",
					["duration_type"] = "seconds",
					["easeStrength"] = 3,
					["easeType"] = "none",
				},
				["finish"] = {
					["type"] = "none",
					["duration_type"] = "seconds",
					["easeStrength"] = 3,
					["easeType"] = "none",
				},
			},
			["desaturate"] = false,
			["discrete_rotation"] = 0,
			["subRegions"] = {
			},
			["height"] = 15,
			["rotate"] = false,
			["load"] = {
				["use_never"] = false,
				["talent"] = {
					["multi"] = {
					},
				},
				["zoneIds"] = "",
				["spec"] = {
					["multi"] = {
					},
				},
				["class"] = {
					["multi"] = {
					},
				},
				["size"] = {
					["multi"] = {
					},
				},
			},
			["textureWrapMode"] = "CLAMPTOBLACKADDITIVE",
			["mirror"] = false,
			["regionType"] = "texture",
			["blendMode"] = "BLEND",
			["texture"] = "Interface\\AddOns\\WeakAuras\\Media\\Textures\\Square_White",
			["anchorFrameType"] = "SCREEN",
			["color"] = {
				1, -- [1]
				0, -- [2]
				0.298039215686275, -- [3]
				1, -- [4]
			},
			["selfPoint"] = "TOPLEFT",
			["tocversion"] = 20501,
			["id"] = "mobtapped/mobdead/spellrange",
			["alpha"] = 1,
			["frameStrata"] = 1,
			["width"] = 20,
			["config"] = {
			},
			["uid"] = "rh8nimpfajU",
			["authorOptions"] = {
			},
			["xOffset"] = 342,
			["conditions"] = {
			},
			["information"] = {
				["ignoreOptionsEventErrors"] = true,
			},
			["rotation"] = 0,
		},
		["x"] = {
			["authorOptions"] = {
			},
			["preferToUpdate"] = false,
			["yOffset"] = 0,
			["anchorPoint"] = "TOPLEFT",
			["actions"] = {
				["start"] = {
				},
				["finish"] = {
				},
				["init"] = {
					["do_custom"] = false,
				},
			},
			["triggers"] = {
				{
					["trigger"] = {
						["type"] = "unit",
						["use_alwaystrue"] = true,
						["subeventSuffix"] = "_CAST_START",
						["duration"] = "1",
						["event"] = "Conditions",
						["names"] = {
						},
						["spellIds"] = {
						},
						["unevent"] = "auto",
						["unit"] = "player",
						["subeventPrefix"] = "SPELL",
						["use_unit"] = true,
						["debuffType"] = "HELPFUL",
					},
					["untrigger"] = {
					},
				}, -- [1]
				["disjunctive"] = "any",
				["activeTriggerMode"] = -10,
			},
			["internalVersion"] = 45,
			["selfPoint"] = "TOPLEFT",
			["desaturate"] = false,
			["discrete_rotation"] = 0,
			["subRegions"] = {
			},
			["height"] = 15,
			["rotate"] = false,
			["load"] = {
				["use_never"] = false,
				["talent"] = {
					["multi"] = {
					},
				},
				["size"] = {
					["multi"] = {
					},
				},
				["spec"] = {
					["multi"] = {
					},
				},
				["class"] = {
					["multi"] = {
					},
				},
				["zoneIds"] = "",
			},
			["textureWrapMode"] = "CLAMPTOBLACKADDITIVE",
			["mirror"] = false,
			["regionType"] = "texture",
			["blendMode"] = "BLEND",
			["texture"] = "Interface\\AddOns\\WeakAuras\\Media\\Textures\\Square_White",
			["width"] = 20,
			["parent"] = "Wow        tg",
			["animation"] = {
				["start"] = {
					["colorR"] = 1,
					["use_scale"] = false,
					["colorB"] = 1,
					["colorG"] = 1,
					["scalex"] = 1,
					["type"] = "custom",
					["use_color"] = true,
					["easeType"] = "none",
					["rotate"] = 0,
					["preset"] = "fade",
					["alpha"] = 0,
					["duration_type"] = "seconds",
					["y"] = 0,
					["x"] = 0,
					["colorType"] = "custom",
					["scaley"] = 1,
					["colorFunc"] = "function(progress, r1, g1, b1, a1, r2, g2, b2, a2)\n    \n    local x,y=C_Map.GetPlayerMapPosition(C_Map.GetBestMapForUnit(\"player\"),\"player\"):GetXY()\n    local xi = math.floor(x*100)/256.\n    local xf = math.floor((x*100 -  math.floor(x*100))*100)/256.\n    return xi, xf,0, 1\nend",
					["easeStrength"] = 3,
					["duration"] = "999999999",
					["colorA"] = 1,
				},
				["main"] = {
					["duration_type"] = "seconds",
					["type"] = "none",
					["easeStrength"] = 3,
					["easeType"] = "none",
				},
				["finish"] = {
					["duration_type"] = "seconds",
					["type"] = "none",
					["easeStrength"] = 3,
					["easeType"] = "none",
				},
			},
			["tocversion"] = 20501,
			["id"] = "x",
			["alpha"] = 1,
			["frameStrata"] = 1,
			["anchorFrameType"] = "SCREEN",
			["config"] = {
			},
			["uid"] = "zeN(SXZ(GoB",
			["xOffset"] = 0,
			["color"] = {
				1, -- [1]
				0, -- [2]
				0.298039215686275, -- [3]
				1, -- [4]
			},
			["conditions"] = {
			},
			["information"] = {
				["ignoreOptionsEventErrors"] = true,
			},
			["rotation"] = 0,
		},
		["food and water"] = {
			["color"] = {
				1, -- [1]
				0, -- [2]
				0.29803921568628, -- [3]
				1, -- [4]
			},
			["preferToUpdate"] = false,
			["yOffset"] = 0,
			["anchorPoint"] = "CENTER",
			["url"] = "",
			["actions"] = {
				["start"] = {
				},
				["finish"] = {
				},
				["init"] = {
				},
			},
			["triggers"] = {
				{
					["trigger"] = {
						["type"] = "unit",
						["use_alwaystrue"] = true,
						["unevent"] = "auto",
						["duration"] = "1",
						["event"] = "Conditions",
						["names"] = {
						},
						["subeventSuffix"] = "_CAST_START",
						["use_ingroup"] = true,
						["spellIds"] = {
						},
						["ingroup"] = {
							["single"] = "solo",
							["multi"] = {
								["solo"] = true,
							},
						},
						["use_unit"] = true,
						["subeventPrefix"] = "SPELL",
						["unit"] = "player",
						["debuffType"] = "HELPFUL",
					},
					["untrigger"] = {
					},
				}, -- [1]
				["disjunctive"] = "any",
				["activeTriggerMode"] = -10,
			},
			["internalVersion"] = 45,
			["selfPoint"] = "TOPLEFT",
			["desaturate"] = false,
			["rotation"] = 0,
			["version"] = 1,
			["subRegions"] = {
			},
			["height"] = 15,
			["rotate"] = false,
			["load"] = {
				["talent"] = {
					["multi"] = {
					},
				},
				["size"] = {
					["multi"] = {
					},
				},
				["class"] = {
					["multi"] = {
					},
				},
				["spec"] = {
					["multi"] = {
					},
				},
				["zoneIds"] = "",
			},
			["textureWrapMode"] = "CLAMPTOBLACKADDITIVE",
			["mirror"] = false,
			["regionType"] = "texture",
			["blendMode"] = "BLEND",
			["alpha"] = 1,
			["texture"] = "Interface\\AddOns\\WeakAuras\\Media\\Textures\\Square_White",
			["parent"] = "Wow        tg",
			["xOffset"] = 209,
			["semver"] = "1.0.0",
			["tocversion"] = 20501,
			["id"] = "food and water",
			["animation"] = {
				["start"] = {
					["colorR"] = 1,
					["use_scale"] = false,
					["colorA"] = 1,
					["colorG"] = 1,
					["type"] = "custom",
					["duration_type"] = "seconds",
					["easeType"] = "none",
					["scalex"] = 1,
					["use_color"] = true,
					["alpha"] = 0,
					["rotate"] = 0,
					["y"] = 0,
					["x"] = 0,
					["scaley"] = 1,
					["duration"] = "36000",
					["colorFunc"] = "function(progress, r1, g1, b1, a1, r2, g2, b2, a2)\n    \n    local CountFood = GetActionCount(8)\n    local CountWater = GetActionCount(9)\n    return 0,CountFood/256,CountWater/256,1\nend\n\n\n\n",
					["easeStrength"] = 3,
					["colorType"] = "custom",
					["colorB"] = 1,
				},
				["main"] = {
					["easeStrength"] = 3,
					["type"] = "none",
					["duration_type"] = "seconds",
					["easeType"] = "none",
				},
				["finish"] = {
					["easeStrength"] = 3,
					["type"] = "none",
					["duration_type"] = "seconds",
					["easeType"] = "none",
				},
			},
			["frameStrata"] = 1,
			["anchorFrameType"] = "SCREEN",
			["config"] = {
			},
			["uid"] = "9dT4hsrbVmr",
			["authorOptions"] = {
			},
			["width"] = 20,
			["conditions"] = {
			},
			["information"] = {
				["ignoreOptionsEventErrors"] = true,
			},
			["discrete_rotation"] = 0,
		},
		["Wow        tg"] = {
			["controlledChildren"] = {
				"x", -- [1]
				"y", -- [2]
				"angle", -- [3]
				"hp/mana/target hp", -- [4]
				"Aggro", -- [5]
				"Target Distance", -- [6]
				"Died", -- [7]
				"Mounted", -- [8]
				"Repair 2", -- [9]
				"Low Bag Space", -- [10]
				"Whisp", -- [11]
				"food and water", -- [12]
				"spell status", -- [13]
				"need to face", -- [14]
				"mob id", -- [15]
				"lootready", -- [16]
				"money count", -- [17]
				"is in game", -- [18]
				"mobtapped/mobdead/spellrange", -- [19]
				"failed attempt", -- [20]
			},
			["borderBackdrop"] = "Blizzard Tooltip",
			["authorOptions"] = {
			},
			["preferToUpdate"] = false,
			["yOffset"] = 0,
			["anchorPoint"] = "TOPLEFT",
			["borderColor"] = {
				0, -- [1]
				0, -- [2]
				0, -- [3]
				1, -- [4]
			},
			["actions"] = {
				["start"] = {
				},
				["init"] = {
				},
				["finish"] = {
				},
			},
			["triggers"] = {
				{
					["trigger"] = {
						["subeventPrefix"] = "SPELL",
						["type"] = "aura2",
						["spellIds"] = {
						},
						["subeventSuffix"] = "_CAST_START",
						["unit"] = "player",
						["names"] = {
						},
						["event"] = "Health",
						["debuffType"] = "HELPFUL",
					},
					["untrigger"] = {
					},
				}, -- [1]
			},
			["internalVersion"] = 45,
			["selfPoint"] = "CENTER",
			["subRegions"] = {
			},
			["load"] = {
				["size"] = {
					["multi"] = {
					},
				},
				["spec"] = {
					["multi"] = {
					},
				},
				["class"] = {
					["multi"] = {
					},
				},
				["talent"] = {
					["multi"] = {
					},
				},
			},
			["backdropColor"] = {
				1, -- [1]
				1, -- [2]
				1, -- [3]
				0.5, -- [4]
			},
			["scale"] = 1,
			["border"] = false,
			["borderEdge"] = "Square Full White",
			["regionType"] = "group",
			["borderSize"] = 2,
			["anchorFrameParent"] = false,
			["borderOffset"] = 4,
			["tocversion"] = 20501,
			["id"] = "Wow        tg",
			["frameStrata"] = 1,
			["anchorFrameType"] = "SCREEN",
			["borderInset"] = 1,
			["uid"] = "ANO15L9RA(R",
			["config"] = {
			},
			["xOffset"] = 0,
			["conditions"] = {
			},
			["information"] = {
				["groupOffset"] = false,
			},
			["animation"] = {
				["start"] = {
					["type"] = "none",
					["easeStrength"] = 3,
					["duration_type"] = "seconds",
					["easeType"] = "none",
				},
				["main"] = {
					["type"] = "none",
					["easeStrength"] = 3,
					["duration_type"] = "seconds",
					["easeType"] = "none",
				},
				["finish"] = {
					["type"] = "none",
					["easeStrength"] = 3,
					["duration_type"] = "seconds",
					["easeType"] = "none",
				},
			},
		},
		["lootready"] = {
			["authorOptions"] = {
			},
			["preferToUpdate"] = false,
			["yOffset"] = 0,
			["anchorPoint"] = "CENTER",
			["url"] = "",
			["actions"] = {
				["start"] = {
				},
				["init"] = {
				},
				["finish"] = {
				},
			},
			["triggers"] = {
				{
					["trigger"] = {
						["type"] = "custom",
						["unevent"] = "timed",
						["custom_hide"] = "timed",
						["duration"] = "2",
						["event"] = "Combat Log",
						["unit"] = "player",
						["custom_type"] = "event",
						["subeventSuffix"] = "",
						["custom"] = "function()\n    if event == LOOT_READY then\n        return true\n    end\nend\n\n\n",
						["subeventPrefix"] = "",
						["events"] = "LOOT_READY LOOT_OPENED",
						["spellIds"] = {
						},
						["names"] = {
						},
						["debuffType"] = "HELPFUL",
					},
					["untrigger"] = {
					},
				}, -- [1]
				["disjunctive"] = "any",
				["activeTriggerMode"] = -10,
			},
			["internalVersion"] = 45,
			["selfPoint"] = "TOPLEFT",
			["desaturate"] = false,
			["rotation"] = 0,
			["version"] = 1,
			["subRegions"] = {
			},
			["height"] = 15,
			["rotate"] = false,
			["load"] = {
				["talent"] = {
					["multi"] = {
					},
				},
				["zoneIds"] = "",
				["class"] = {
					["multi"] = {
					},
				},
				["spec"] = {
					["multi"] = {
					},
				},
				["size"] = {
					["multi"] = {
					},
				},
			},
			["textureWrapMode"] = "CLAMPTOBLACKADDITIVE",
			["mirror"] = false,
			["regionType"] = "texture",
			["blendMode"] = "BLEND",
			["frameStrata"] = 1,
			["texture"] = "Interface\\AddOns\\WeakAuras\\Media\\Textures\\Square_White",
			["parent"] = "Wow        tg",
			["color"] = {
				0, -- [1]
				0, -- [2]
				0, -- [3]
				1, -- [4]
			},
			["semver"] = "1.0.0",
			["tocversion"] = 20501,
			["id"] = "lootready",
			["animation"] = {
				["start"] = {
					["colorR"] = 1,
					["use_scale"] = false,
					["colorA"] = 1,
					["colorG"] = 1,
					["type"] = "custom",
					["colorB"] = 1,
					["easeType"] = "none",
					["x"] = 0,
					["use_color"] = true,
					["alpha"] = 0,
					["easeStrength"] = 3,
					["y"] = 0,
					["colorType"] = "custom",
					["scaley"] = 1,
					["duration"] = "36000",
					["colorFunc"] = "\n\n",
					["rotate"] = 0,
					["scalex"] = 1,
					["duration_type"] = "seconds",
				},
				["main"] = {
					["type"] = "none",
					["easeStrength"] = 3,
					["duration_type"] = "seconds",
					["easeType"] = "none",
				},
				["finish"] = {
					["type"] = "none",
					["easeStrength"] = 3,
					["duration_type"] = "seconds",
					["easeType"] = "none",
				},
			},
			["alpha"] = 1,
			["anchorFrameType"] = "SCREEN",
			["width"] = 20,
			["uid"] = "qMKdGQlznnI",
			["xOffset"] = 285,
			["config"] = {
			},
			["conditions"] = {
			},
			["information"] = {
				["ignoreOptionsEventErrors"] = true,
			},
			["discrete_rotation"] = 0,
		},
		["Repair 2"] = {
			["outline"] = "THICKOUTLINE",
			["authorOptions"] = {
			},
			["displayText"] = "%c",
			["customText"] = "function() \n    local i,cur,max,min;\n    min=100;\n    for i=1,10 do\n        cur,max=GetInventoryItemDurability(i);\n        if cur and (cur/max) <= 0.5 then\n            if  (cur/max)*100<min then\n                min=(cur/max)*100;\n            end\n        end\n        \n    end\n    return \"Durability =\"..math.floor(min)..\"%%\";\nend\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n",
			["shadowYOffset"] = -1,
			["anchorPoint"] = "CENTER",
			["customTextUpdate"] = "event",
			["url"] = "",
			["actions"] = {
				["start"] = {
					["sound"] = "Interface\\Addons\\WeakAuras\\PowerAurasMedia\\Sounds\\chant4.ogg",
					["do_sound"] = false,
				},
				["finish"] = {
				},
				["init"] = {
				},
			},
			["triggers"] = {
				{
					["trigger"] = {
						["itemName"] = 1,
						["subeventPrefix"] = "UNIT_DIED",
						["custom_hide"] = "custom",
						["unit"] = "player",
						["type"] = "custom",
						["names"] = {
						},
						["custom_type"] = "event",
						["use_unit"] = true,
						["use_itemName"] = true,
						["event"] = "Combat Log",
						["unevent"] = "timed",
						["events"] = "PLAYER_TARGET_CHANGED,COMBAT_LOG_EVENT,PLAYER_DEAD,UPDATE_INVENTORY_DURABILITY",
						["spellIds"] = {
						},
						["custom"] = "function() local i,cur,max;\n    for i=1,18 do\n        cur,max=GetInventoryItemDurability(i);\n        if cur and max then\n            if (cur/max) <= 0.5\n            then\n                return true;\n            end\n        end\n    end\n    return false;\nend",
						["use_sourceUnit"] = true,
						["check"] = "update",
						["subeventSuffix"] = "",
						["sourceUnit"] = "player",
						["debuffType"] = "HELPFUL",
					},
					["untrigger"] = {
						["custom"] = "function() local i,cur,max;\n    for i=1,18 do\n        cur,max=GetInventoryItemDurability(i);\n        if cur and max then\n            if (cur/max) <= 0.5 then\n                return false;\n            end\n        end\n    end\n    return true;\nend",
					},
				}, -- [1]
				{
					["trigger"] = {
						["type"] = "custom",
						["custom_type"] = "event",
						["duration"] = "2",
						["event"] = "Conditions",
						["unit"] = "player",
						["events"] = "ACTIONBAR_PAGE_CHANGED",
						["custom_hide"] = "timed",
						["custom"] = "function()\nreturn true;\nend",
						["use_unit"] = true,
						["debuffType"] = "HELPFUL",
					},
					["untrigger"] = {
					},
				}, -- [2]
				["disjunctive"] = "any",
				["activeTriggerMode"] = 1,
			},
			["internalVersion"] = 45,
			["wordWrap"] = "WordWrap",
			["desaturate"] = false,
			["discrete_rotation"] = 0,
			["font"] = "Friz Quadrata TT",
			["version"] = 3,
			["subRegions"] = {
			},
			["height"] = 15,
			["rotate"] = false,
			["load"] = {
				["use_never"] = false,
				["talent"] = {
					["multi"] = {
					},
				},
				["spec"] = {
					["multi"] = {
					},
				},
				["difficulty"] = {
					["multi"] = {
					},
				},
				["role"] = {
					["multi"] = {
					},
				},
				["size"] = {
					["multi"] = {
					},
				},
				["faction"] = {
					["multi"] = {
					},
				},
				["pvptalent"] = {
					["multi"] = {
					},
				},
				["race"] = {
					["multi"] = {
					},
				},
				["class"] = {
					["single"] = "HUNTER",
					["multi"] = {
						["HUNTER"] = true,
					},
				},
				["zoneIds"] = "",
			},
			["color"] = {
				0.972549019607843, -- [1]
				0.603921568627451, -- [2]
				0.494117647058824, -- [3]
				1, -- [4]
			},
			["config"] = {
			},
			["textureWrapMode"] = "CLAMPTOBLACKADDITIVE",
			["preferToUpdate"] = false,
			["shadowXOffset"] = 1,
			["yOffset"] = 0,
			["mirror"] = false,
			["fixedWidth"] = 200,
			["regionType"] = "texture",
			["automaticWidth"] = "Auto",
			["blendMode"] = "BLEND",
			["rotation"] = 0,
			["parent"] = "Wow        tg",
			["texture"] = "Interface\\AddOns\\WeakAuras\\Media\\Textures\\Square_White",
			["anchorFrameType"] = "SCREEN",
			["alpha"] = 1,
			["justify"] = "LEFT",
			["tocversion"] = 20501,
			["id"] = "Repair 2",
			["animation"] = {
				["start"] = {
					["type"] = "custom",
					["easeType"] = "none",
					["duration_type"] = "seconds",
					["preset"] = "spiral",
					["easeStrength"] = 3,
				},
				["main"] = {
					["type"] = "none",
					["easeType"] = "none",
					["preset"] = "alphaPulse",
					["duration_type"] = "seconds",
					["easeStrength"] = 3,
				},
				["finish"] = {
					["duration_type"] = "seconds",
					["type"] = "none",
					["easeStrength"] = 3,
					["easeType"] = "none",
				},
			},
			["frameStrata"] = 1,
			["width"] = 20,
			["semver"] = "1.0.0",
			["uid"] = "lMYn13B68p2",
			["selfPoint"] = "TOPLEFT",
			["xOffset"] = 152,
			["shadowColor"] = {
				0, -- [1]
				0, -- [2]
				0, -- [3]
				1, -- [4]
			},
			["conditions"] = {
			},
			["information"] = {
				["ignoreOptionsEventErrors"] = true,
			},
			["fontSize"] = 34,
		},
		["spell status"] = {
			["color"] = {
				0, -- [1]
				0, -- [2]
				0, -- [3]
				1, -- [4]
			},
			["preferToUpdate"] = false,
			["yOffset"] = 0,
			["anchorPoint"] = "CENTER",
			["url"] = "",
			["actions"] = {
				["start"] = {
				},
				["finish"] = {
				},
				["init"] = {
					["do_custom"] = true,
					["custom"] = "aura_env.playerGUID = UnitGUID(\"player\")\naura_env.SpellStatus = 0\n\n\n",
				},
			},
			["triggers"] = {
				{
					["trigger"] = {
						["ingroup"] = {
							["single"] = "solo",
							["multi"] = {
								["solo"] = true,
							},
						},
						["use_alwaystrue"] = true,
						["duration"] = "0.5",
						["names"] = {
						},
						["debuffType"] = "HELPFUL",
						["custom_hide"] = "timed",
						["type"] = "custom",
						["subeventSuffix"] = "_CAST_START",
						["custom_type"] = "event",
						["use_unit"] = true,
						["unevent"] = "auto",
						["event"] = "Conditions",
						["events"] = "COMBAT_LOG_EVENT_UNFILTERED",
						["customDuration"] = "function()\n    return aura_env.SpellStatus, 10, true\nend",
						["use_ingroup"] = true,
						["spellIds"] = {
						},
						["custom"] = "function (event, _, subtype, _, sourceGUID, _, _, _, destGUID, _, _, _, spellID, spellName)\n    if not sourceGUID then return end\n    local npcType = select(1, strsplit(\"-\", sourceGUID))\n    if sourceGUID == aura_env.playerGUID then\n        if subtype == \"SPELL_CAST_SUCCESS\" then\n            aura_env.SpellStatus = 1\n            --    print(\"Spell Success\")\n        elseif subtype == \"SPELL_CAST_FAILED\" then\n            aura_env.SpellStatus = 2\n            --    print(\"Spell Failed\")\n        elseif subtype == \"SPELL_CAST_START\" then\n            aura_env.SpellStatus = 0\n            --    print(\"Spell Start\"..\" by \"..sourceGUID)\n        end\n        return aura_env.SpellStatus\n    end\nend\n\n\n--aura_env.SpellSuccess = 0\n--aura_env.SpellFailed = 0\n\n\n\n",
						["check"] = "event",
						["subeventPrefix"] = "SPELL",
						["unit"] = "player",
						["dynamicDuration"] = false,
					},
					["untrigger"] = {
						["custom"] = "function()\n    return aura_env.SpellStatus == 0\nend",
					},
				}, -- [1]
				["disjunctive"] = "any",
				["activeTriggerMode"] = -10,
			},
			["internalVersion"] = 45,
			["animation"] = {
				["start"] = {
					["colorR"] = 1,
					["duration_type"] = "seconds",
					["colorB"] = 1,
					["colorG"] = 1,
					["type"] = "custom",
					["scalex"] = 1,
					["easeType"] = "none",
					["use_scale"] = false,
					["use_color"] = true,
					["alpha"] = 0,
					["rotate"] = 0,
					["y"] = 0,
					["x"] = 0,
					["scaley"] = 1,
					["duration"] = "0.5",
					["colorFunc"] = "function(progress, r1, g1, b1, a1, r2, g2, b2, a2)\n    if aura_env.SpellStatus == 0 then -- start\n        return 0,0,0,1\n    elseif aura_env.SpellStatus == 1 then -- success\n        return 1/256,0,0,1\n    elseif aura_env.SpellStatus == 2 then -- failed\n        return 2/256,0,0,1\n    end\nend",
					["easeStrength"] = 3,
					["colorType"] = "custom",
					["colorA"] = 1,
				},
				["main"] = {
					["easeStrength"] = 3,
					["type"] = "none",
					["duration_type"] = "seconds",
					["easeType"] = "none",
				},
				["finish"] = {
					["colorR"] = 0,
					["scalex"] = 1,
					["colorB"] = 0,
					["colorG"] = 0,
					["type"] = "none",
					["easeType"] = "none",
					["duration"] = "36000",
					["use_color"] = true,
					["alpha"] = 0,
					["colorType"] = "straightColor",
					["y"] = 0,
					["x"] = 0,
					["easeStrength"] = 3,
					["scaley"] = 1,
					["colorFunc"] = "function(progress, r1, g1, b1, a1, r2, g2, b2, a2)\n    return r1 + (progress * (r2 - r1)), g1 + (progress * (g2 - g1)), b1 + (progress * (b2 - b1)), a1 + (progress * (a2 - a1))\nend\n",
					["rotate"] = 0,
					["colorA"] = 1,
					["duration_type"] = "seconds",
				},
			},
			["desaturate"] = false,
			["rotation"] = 0,
			["version"] = 1,
			["subRegions"] = {
			},
			["height"] = 15,
			["rotate"] = false,
			["load"] = {
				["use_never"] = false,
				["talent"] = {
					["multi"] = {
					},
				},
				["size"] = {
					["multi"] = {
					},
				},
				["class"] = {
					["multi"] = {
					},
				},
				["spec"] = {
					["multi"] = {
					},
				},
				["zoneIds"] = "",
			},
			["textureWrapMode"] = "CLAMPTOBLACKADDITIVE",
			["mirror"] = false,
			["regionType"] = "texture",
			["blendMode"] = "BLEND",
			["frameStrata"] = 1,
			["texture"] = "Interface\\AddOns\\WeakAuras\\Media\\Textures\\Square_White",
			["parent"] = "Wow        tg",
			["xOffset"] = 228,
			["semver"] = "1.0.0",
			["tocversion"] = 20501,
			["id"] = "spell status",
			["discrete_rotation"] = 0,
			["alpha"] = 1,
			["anchorFrameType"] = "SCREEN",
			["uid"] = "6KA3Yi6zkKT",
			["config"] = {
			},
			["authorOptions"] = {
			},
			["width"] = 20,
			["conditions"] = {
			},
			["information"] = {
				["ignoreOptionsEventErrors"] = true,
			},
			["selfPoint"] = "TOPLEFT",
		},
		["is in game"] = {
			["authorOptions"] = {
			},
			["preferToUpdate"] = false,
			["yOffset"] = 0,
			["anchorPoint"] = "CENTER",
			["actions"] = {
				["start"] = {
				},
				["finish"] = {
				},
				["init"] = {
					["do_custom"] = false,
				},
			},
			["triggers"] = {
				{
					["trigger"] = {
						["type"] = "unit",
						["use_alwaystrue"] = true,
						["subeventSuffix"] = "_CAST_START",
						["event"] = "Conditions",
						["duration"] = "1",
						["threatUnit"] = "focus",
						["unit"] = "target",
						["use_threatUnit"] = true,
						["use_status"] = false,
						["spellIds"] = {
						},
						["unevent"] = "auto",
						["subeventPrefix"] = "SPELL",
						["use_unit"] = true,
						["names"] = {
						},
						["debuffType"] = "HELPFUL",
					},
					["untrigger"] = {
					},
				}, -- [1]
				["disjunctive"] = "any",
				["activeTriggerMode"] = -10,
			},
			["internalVersion"] = 45,
			["selfPoint"] = "TOPLEFT",
			["desaturate"] = false,
			["discrete_rotation"] = 0,
			["subRegions"] = {
			},
			["height"] = 15,
			["rotate"] = false,
			["load"] = {
				["use_never"] = false,
				["talent"] = {
					["multi"] = {
					},
				},
				["size"] = {
					["multi"] = {
					},
				},
				["spec"] = {
					["multi"] = {
					},
				},
				["class"] = {
					["multi"] = {
					},
				},
				["zoneIds"] = "",
			},
			["textureWrapMode"] = "CLAMPTOBLACKADDITIVE",
			["mirror"] = false,
			["regionType"] = "texture",
			["blendMode"] = "BLEND",
			["texture"] = "Interface\\AddOns\\WeakAuras\\Media\\Textures\\Square_White",
			["width"] = 20,
			["parent"] = "Wow        tg",
			["animation"] = {
				["start"] = {
					["colorR"] = 1,
					["use_scale"] = false,
					["colorB"] = 1,
					["colorG"] = 1,
					["scalex"] = 1,
					["type"] = "custom",
					["preset"] = "fade",
					["easeType"] = "none",
					["rotate"] = 0,
					["use_color"] = false,
					["alpha"] = 0,
					["duration_type"] = "seconds",
					["y"] = 0,
					["x"] = 0,
					["colorType"] = "custom",
					["scaley"] = 1,
					["colorFunc"] = "function(progress, r1, g1, b1, a1, r2, g2, b2, a2)\n    \n    local money = GetMoney() / 10000\n    \n    local hash3 = money % 256\n    local hash2 = ((money - hash3) / 256) % 256\n    local hash1 = math.floor(((money - hash3) / 256) / 256)\n    local hash3conv = hash3 / 256.\n    local hash2conv = hash2 / 256.\n    local hash1conv = hash1 / 256.\n    \n    return hash1conv, hash2conv, hash3conv, 1\nend",
					["easeStrength"] = 3,
					["duration"] = "999999999",
					["colorA"] = 1,
				},
				["main"] = {
					["duration_type"] = "seconds",
					["type"] = "none",
					["easeStrength"] = 3,
					["easeType"] = "none",
				},
				["finish"] = {
					["duration_type"] = "seconds",
					["type"] = "none",
					["easeStrength"] = 3,
					["easeType"] = "none",
				},
			},
			["tocversion"] = 20501,
			["id"] = "is in game",
			["alpha"] = 1,
			["frameStrata"] = 1,
			["anchorFrameType"] = "SCREEN",
			["uid"] = "t)ntONRGMY0",
			["config"] = {
			},
			["xOffset"] = 323,
			["color"] = {
				0, -- [1]
				0, -- [2]
				0, -- [3]
				1, -- [4]
			},
			["conditions"] = {
			},
			["information"] = {
				["ignoreOptionsEventErrors"] = true,
			},
			["rotation"] = 0,
		},
	},
	["mousePointerFrame"] = {
		["xOffset"] = -152.1151123046875,
		["yOffset"] = -389.0148010253906,
	},
	["registered"] = {
	},
	["lastArchiveClear"] = 1628177066,
	["minimap"] = {
		["minimapPos"] = 199.2353717995835,
		["hide"] = false,
	},
	["lastUpgrade"] = 1628177069,
	["dbVersion"] = 45,
	["editor_bracket_matching"] = true,
	["login_squelch_time"] = 10,
	["frame"] = {
		["xOffset"] = -139.8709716796875,
		["width"] = 830.0001831054688,
		["height"] = 500.0000915527344,
		["yOffset"] = -84.47552490234375,
	},
	["editor_theme"] = "Standard",
}
