<template>
  <div id="permission">
    <el-form id="toolbar" :inline="true" :model="searchForm" class="demo-form-inline">
      <el-form-item label="功能名称">
        <el-input v-model="searchForm.PermissionName" placeholder="请输入功能">
        </el-input>
      </el-form-item>
      <el-form-item label="前端路由">
        <el-input v-model="searchForm.PermissionRoute" placeholder="请输入前端路由地址">
        </el-input>
      </el-form-item>
      <el-form-item label="后端路由">
        <el-input v-model="searchForm.PermissionApi" placeholder="请输入后端路由地址">
        </el-input>
      </el-form-item>
      <el-form-item>
        <el-button @click="find()" plain>查询</el-button>
        <el-button type="info" @click="findReset()" plain>重置</el-button>
      </el-form-item>
    </el-form>
    <div id="datagrid">
      <div class="toolbar">
        <el-button plain icon="el-icon-plus" @click="add()">添加功能</el-button>
      </div>

      <el-table :data="tableData" border style="width: 100%" @select-all="selectChange" @selection-change="selectChange" v-loading="loading">
        <el-table-column prop="permissionId" label="序号" width="50">
        </el-table-column>
        <el-table-column prop="permissionName" label="功能名称">
        </el-table-column>
        <el-table-column prop="permissionRoute" label="前端路由" width="150">
        </el-table-column>
        <el-table-column prop="permissionApi" label="后端路由" width="180">
        </el-table-column>
        <el-table-column prop="parentId" v-if="false" label="父功能Id" width="180">
        </el-table-column>
        <el-table-column prop="parentIdName" label="所属功能" width="180">
        </el-table-column>
        <el-table-column prop="permissionDecs" label="备注">
        </el-table-column>
        <el-table-column label="操作">
          <template slot-scope="scope">
            <el-tooltip content="编辑功能" placement="right-end" effect="light">
              <el-button plain icon="el-icon-edit" type="primary" size="small" @click="edit(scope.row)"></el-button>
            </el-tooltip>
            <el-tooltip content="删除" placement="right-end" effect="light">
              <el-button plain icon="el-icon-delete" type="danger" size="small" @click="del(scope.row)"></el-button>
            </el-tooltip>
          </template>
        </el-table-column>
      </el-table>
      <!-- 分页 -->
      <el-row class="page">
        <el-col :span="20">
          <el-pagination background @current-change="pagination" @size-change="sizeChange" :current-page.sync="current_page" :page-sizes="[10,20,25,50]" layout="total,sizes,prev, pager, next" :page-size.sync="pageSize" :total="total">
          </el-pagination>
        </el-col>
      </el-row>
    </div>

    <!-- 编辑列表 -->
    <el-dialog title="功能管理" center :rules="rules" ref="permission" :visible.sync="editDialogFormVisible" :close-on-click-modal="false">
      <el-form :model="form" label-width="100px">
        <el-row>
          <el-col :span="12">
            <el-form-item label="功能名称" prop="name">
              <el-input v-model="form.permissionName" placeholder="请输入名称">
              </el-input>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="功能描述">
              <el-input type="textarea" :autosize="{ minRows: 2, maxRows: 4}" v-model="form.permissionDecs"></el-input>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="12">
            <el-form-item label="前端路由">
              <el-input v-model="form.permissionRoute"></el-input>
            </el-form-item>
          </el-col>
          <el-col :span="12">
            <el-form-item label="后端路由">
              <el-input v-model="form.permissionApi"></el-input>
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="24">
            <el-form-item label="所属功能">
                <template>
                  <el-select v-model="form.parentId" placeholder="请选择">
                    <el-option v-for="item in permissionList" :key="item.permissionId" :label="item.permissionName" :value="item.permissionId">
                      <span style="float: left">{{ item.permissionName }}</span>
                    </el-option>
                  </el-select>
                </template>
          </el-form-item>
          </el-col>
        </el-row>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button @click="cancel()">取 消</el-button>
        <el-button type="primary" @click="save()">确 定</el-button>
      </div>
    </el-dialog>

  </div>
</template>

<script>
import {
  getInfo,
  getInfoById,
  addInfo,
  updateInfo,
  deleteInfoById,
  Model,
  getPermission,
  SearchModel
} from "@/api/permission";
import {config} from "./../../config/index"
import {Tools} from "@/views/utils/Tools";

export default {
  data() {
    return {
      searchForm: new SearchModel(),
      tableData: [],
      editDialogFormVisible: false,
      uploadId: "",
      teachers: [],
      permissions: [],
      form: new Model(),
      loading: false,
      isNew: false,
      isEdit: false,
      current_page: 1,
      total: 0,
      pageSize: 10,
      permissionList:[],
      rules:{
          name: [
            {required: true ,message: '请输入功能名称', trigger: 'blur'}
          ]
      }
    };
  },
  methods: {
    // 搜索相关
    find() {
       this.fetchData()
    },
    findReset() {
      this. searchForm = new SearchModel()
      this.fetchData()
    },
    // 查询数据 获取信息列表
    fetchData(searchObj = this.searchForm , page = this.current_page, pageSize = this.pageSize) {
      this.loading = true
      getInfo(searchObj, page, pageSize)
        .then(response => {
          //成功执行内容
          let result = response.data;
           this.tableData = result;
           this.total = response.total;
           this.loading = false
        })
        .catch(err => {
          this.loading = false
        });
    },
    add() {
      this.editDialogFormVisible = true;
      this.isNew = true;
      this.form = new Model(null,null,2);
    },
    // 模型的新建、编辑与更新
    edit(row) {
      let id = row.permissionId;
      this.uploadId = id;
      getInfoById(id).then(response => {
        let result = response.data;
        this.form = result;
        this.isEdit = true;
        if (row.type === 1) {
            this.isGroup = true
        } else {
            this.editDialogFormVisible = true;
        }

      });
    },
    save() {
      this.editDialogFormVisible = false;
      if (this.isNew) {
        this.isNew = false
        this.newData()
      }
      if (this.isEdit) {
        this.isEdit = false
        this.updateData()
      }
    },
    cancel(){
      this.editDialogFormVisible = false
      this.isNew = false
      this.isEdit = false
    },
    updateData() {
      updateInfo(this.uploadId, this.form)
        .then(response => {
          //成功执行内容
          let result = response.status_code;
          if (result == 200) {
            // let currentId = this.form.id;
            // let record = 0;
            // record = this.tableData.findIndex((val, index) => {
            //   return val.id == currentId;
            // });
            // this.tableData.splice(record, 1, this.form);
            this.fetchData();
            Tools.success(this, "信息更新成功");
          }
        })
        .catch(err => {
          Tools.error(this, err.response.data)
        });
    },
    newData() {
      addInfo(this.form)
        .then(response => {
            Tools.success(this, "功能信息添加成功");
            this.fetchData();
        })
        .catch(err => {
          Tools.error(this, err.response.data)
        });
    },

    // 删除  删除单个 批量删除
    del(row) {
      this.$confirm("此操作将永久删除该信息, 是否继续?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning"
      }).then(() => {
          let id = row.permissionId;
          let record = null;
          record = this.tableData.findIndex(val => val.id == id);
          deleteInfoById(id).then(response => {
              let result = response.status_code;
              if (result == 200) {
                this.$message({
                  type: "success",
                  message: "删除成功!"
                });
                this.tableData.splice(record, 1);
                this.fetchData();
              }
            }).catch(err => {
              //失败执行内容
              Tools.error(this, err.response.data)
        })
      })
    },
    selectChange(selection) {
       this.multiSelect = selection;
    },
    // 分页相关
    pagination(val) {
      this.current_page = val;
      this.fetchData()
    },
    sizeChange(val) {
       this.pageSize = val;
       this.fetchData()
    },

  },
  mounted() { // 操作相关的DOM
  },
  created() { // 获取数据，无法操作节点
    getPermission().then(res => {
      this.permissionList = res.data;
      this.fetchData();
    });
  },
};
</script>

<style lang="scss">
@import "./../../styles/app-main.scss";
#leader .el-input {
    width: 200px;
    margin-left: 10px;
}
</style>
